﻿using Hangfire;
using Microsoft.Net.Http.Headers;
using WebApp.Models.DTOs;

namespace WebApp.Lib
{
    public class SubjectDownloader
    {
        private readonly IConfiguration _Configuration;
        private readonly IHttpClientFactory _HttpClientFactory;

        public SubjectDownloader(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _Configuration = configuration;
            _HttpClientFactory = httpClientFactory;
        }

        public void Start()
        {
            RecurringJob.AddOrUpdate(nameof(SubjectDownloader), () => Run(), Cron.MinuteInterval(10));
        }

        public void Run()
        {
            HttpClient httpClient = GetClient();

            ReadUser(httpClient);

            ReadSummary(httpClient);
        }

        private void ReadUser(HttpClient httpClient)
        {
            var response = httpClient.GetAsync("user").Result;

            if (!response.IsSuccessStatusCode) return;

            var user = response.Content.ReadFromJsonAsync<UserDTO>().Result;

            if (user == null) return;

            using var context = new WebAppContext();

            var userRecord = context.Users.FirstOrDefault(u => u.Username == user.Data.Username);

            if (userRecord == null)
            {
                context.Users.Add(new Models.User
                {
                    Level = user.Data.Level,
                    StartedAt = user.Data.StartedAt,
                    Username = user.Data.Username
                });
            }
            else
            {
                userRecord.Level = user.Data.Level;
            }

            context.SaveChanges();
        }

        private void ReadSummary(HttpClient httpClient)
        {
            var response = httpClient.GetAsync("summary").Result;

            if (!response.IsSuccessStatusCode) return;

            var summary = response.Content.ReadFromJsonAsync<SummaryDTO>().Result;

            if (summary == null) return;

            response = httpClient.GetAsync($"subjects?ids={string.Join(",", summary.Data.Reviews.SelectMany(r => r.SubjectIds))}").Result;

            if (!response.IsSuccessStatusCode) return;

            var subjects = response.Content.ReadFromJsonAsync<SubjectsDTO>().Result;

            if (subjects == null) return;

            using var context = new WebAppContext();

            foreach (var subject in subjects.Data.Where(d => !string.IsNullOrWhiteSpace(d.Data.Characters)))
            {
                var subjectRecord = context.Subjects.FirstOrDefault(s => s.RemoteId == subject.Id);

                if (subjectRecord == null)
                {
                    var newSubject = context.Subjects.Add(new Models.Subject
                    {
                        Characters = subject.Data.Characters,
                        CreatedAt = subject.Data.CreatedAt,
                        HiddenAt = subject.Data.HiddenAt,
                        Level = subject.Data.Level,
                        RemoteId = subject.Id
                    });

                    newSubject.Entity.Meanings.AddRange(subject.Data.Meanings.Select(m => new Models.SubjectMeaning
                    {
                        AcceptedAnswer = m.AcceptedAnswer,
                        Meaning = m.Meaning,
                        Primary = m.Primary
                    }));

                    newSubject.Entity.Readings.AddRange(subject.Data.Readings.Select(m => new Models.SubjectReading
                    {
                        AcceptedAnswer = m.AcceptedAnswer,
                        Reading = m.Reading,
                        Type = m.Type,
                        Primary = m.Primary
                    }));
                }
            }

            context.SaveChanges();
        }

        private HttpClient GetClient()
        {
            var httpClient = _HttpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri("https://api.wanikani.com/v2/");
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {_Configuration["WaniKani:UserApiKey"]}");
            httpClient.DefaultRequestHeaders.Add("Wanikani-Revision", "20170710");

            return httpClient;
        }
    }
}