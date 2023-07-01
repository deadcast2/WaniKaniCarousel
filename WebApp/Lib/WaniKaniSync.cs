using Hangfire;
using Microsoft.Net.Http.Headers;
using WebApp.Models;
using WebApp.Models.DTOs;

namespace WebApp.Lib
{
    public class WaniKaniSync
    {
        private readonly IConfiguration _Configuration;
        private readonly IHttpClientFactory _HttpClientFactory;

        public WaniKaniSync(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _Configuration = configuration;
            _HttpClientFactory = httpClientFactory;
        }

        public void Start()
        {
            RecurringJob.AddOrUpdate(nameof(WaniKaniSync), () => Run(), Cron.MinuteInterval(10));
        }

        public void Run()
        {
            using var context = new WebAppContext();

            var user = GetUser(context);

            UpdateUser(user, GetSummary(user, context));

            context.SaveChanges();
        }

        private User GetUser(WebAppContext context)
        {
            if (string.IsNullOrWhiteSpace(_Configuration["WaniKani:UserApiKey"]))
                throw new Exception("User api key must be set.");

            var userRecord = context.Users.FirstOrDefault(u => u.ApiKey == _Configuration["WaniKani:UserApiKey"]);

            userRecord ??= context.Users.Add(new User
            {
                ApiKey = $"{_Configuration["WaniKani:UserApiKey"]}"
            }).Entity;

            return userRecord;
        }

        private SummaryDTO GetSummary(User user, WebAppContext context)
        {
            var httpClient = GetClient();
            var response = httpClient.GetAsync("summary").Result;

            if (!response.IsSuccessStatusCode) return new();

            var summary = response.Content.ReadFromJsonAsync<SummaryDTO>().Result;

            if (summary == null) return new();

            response = httpClient.GetAsync($"subjects?ids={string.Join(",", summary.Data.Reviews.SelectMany(r => r.SubjectIds))}").Result;

            if (!response.IsSuccessStatusCode) return new();

            var subjects = response.Content.ReadFromJsonAsync<SubjectsDTO>().Result;

            if (subjects == null) return new();

            foreach (var subject in subjects.Data)
            {
                var subjectRecord = user.Subjects.FirstOrDefault(s => s.RemoteId == subject.Id);

                if (subjectRecord == null)
                {
                    var newSubject = context.Subjects.Add(new Subject
                    {
                        User = user,
                        Characters = subject.Data.Characters ?? string.Empty,
                        ImageData = DownloadFile(subject.Data.CharacterImages),
                        CreatedAt = subject.Data.CreatedAt,
                        UpdatedAt = DateTime.UtcNow,
                        HiddenAt = subject.Data.HiddenAt,
                        Level = subject.Data.Level,
                        RemoteId = subject.Id,
                        SeenLock = context.Subjects.Select(s => s.SeenLock).DefaultIfEmpty(1).Max() - 1
                    });

                    context.SubjectMeanings.AddRange(subject.Data.Meanings.Select(m => new SubjectMeaning
                    {
                        Subject = newSubject.Entity,
                        AcceptedAnswer = m.AcceptedAnswer,
                        Meaning = m.Meaning,
                        Primary = m.Primary
                    }));

                    context.SubjectReadings.AddRange(subject.Data.Readings.Select(m => new SubjectReading
                    {
                        Subject = newSubject.Entity,
                        AcceptedAnswer = m.AcceptedAnswer,
                        Reading = m.Reading,
                        Type = m.Type,
                        Primary = m.Primary
                    }));
                }
                else
                {
                    // This subject must be important to study again since the response provided it.
                    subjectRecord.UpdatedAt = DateTime.UtcNow;
                }
            }

            return summary;
        }

        private void UpdateUser(User user, SummaryDTO summary)
        {
            user.AvailableLessonsCount = summary.Data.Lessons.Sum(r => r.SubjectIds.Count);
            user.AvailableReviewsCount = summary.Data.Reviews.Where(r => r.AvailableAt < DateTime.UtcNow).Sum(r => r.SubjectIds.Count);

            var response = GetClient().GetAsync("user").Result;

            if (!response.IsSuccessStatusCode) return;

            var userInfo = response.Content.ReadFromJsonAsync<UserDTO>().Result;

            if (userInfo == null) return;

            user.Username = userInfo.Data.Username;
            user.Level = userInfo.Data.Level;
            user.StartedAt = userInfo.Data.StartedAt;
        }

        private string? DownloadFile(List<SubjectCharacterImageDTO> characterImages)
        {
            var image = characterImages.Find(m => m.ContentType.Contains("svg") && m.Metadata.InlineStyles);

            if (image == null) return null;

            return new HttpClient().GetStringAsync(image.Url).Result;
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
