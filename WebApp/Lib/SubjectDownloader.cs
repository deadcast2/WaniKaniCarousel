using Hangfire;
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

            var summary = ReadSummary(httpClient);

            ReadUser(summary, httpClient);
        }

        private void ReadUser(SummaryDTO summary, HttpClient httpClient)
        {
            var response = httpClient.GetAsync("user").Result;

            if (!response.IsSuccessStatusCode) return;

            var user = response.Content.ReadFromJsonAsync<UserDTO>().Result;

            if (user == null) return;

            using var context = new WebAppContext();

            var userRecord = context.Users.FirstOrDefault(u => u.Username == user.Data.Username);
            var availableReviewsCount = summary.Data.Reviews.Where(r => r.AvailableAt < DateTime.UtcNow).Sum(r => r.SubjectIds.Count);
            var availableLessonsCount = summary.Data.Lessons.Sum(r => r.SubjectIds.Count);

            if (userRecord == null)
            {
                context.Users.Add(new Models.User
                {
                    Level = user.Data.Level,
                    StartedAt = user.Data.StartedAt,
                    Username = user.Data.Username,
                    AvailableReviewsCount = availableReviewsCount,
                    AvailableLessonsCount = availableLessonsCount
                });
            }
            else
            {
                userRecord.Level = user.Data.Level;
                userRecord.AvailableReviewsCount = availableReviewsCount;
                userRecord.AvailableLessonsCount = availableLessonsCount;
            }

            context.SaveChanges();
        }

        private SummaryDTO ReadSummary(HttpClient httpClient)
        {
            var response = httpClient.GetAsync("summary").Result;

            if (!response.IsSuccessStatusCode) return new();

            var summary = response.Content.ReadFromJsonAsync<SummaryDTO>().Result;

            if (summary == null) return new();

            response = httpClient.GetAsync($"subjects?ids={string.Join(",", summary.Data.Reviews.SelectMany(r => r.SubjectIds))}").Result;

            if (!response.IsSuccessStatusCode) return new();

            var subjects = response.Content.ReadFromJsonAsync<SubjectsDTO>().Result;

            if (subjects == null) return new();

            using var context = new WebAppContext();

            foreach (var subject in subjects.Data)
            {
                var subjectRecord = context.Subjects.FirstOrDefault(s => s.RemoteId == subject.Id);

                if (subjectRecord == null)
                {
                    var newSubject = context.Subjects.Add(new Models.Subject
                    {
                        Characters = subject.Data.Characters ?? string.Empty,
                        ImageData = DownloadFile(subject.Data.CharacterImages),
                        CreatedAt = subject.Data.CreatedAt,
                        HiddenAt = subject.Data.HiddenAt,
                        Level = subject.Data.Level,
                        RemoteId = subject.Id
                    });

                    context.SubjectMeanings.AddRange(subject.Data.Meanings.Select(m => new Models.SubjectMeaning
                    {
                        Subject = newSubject.Entity,
                        AcceptedAnswer = m.AcceptedAnswer,
                        Meaning = m.Meaning,
                        Primary = m.Primary
                    }));

                    context.SubjectReadings.AddRange(subject.Data.Readings.Select(m => new Models.SubjectReading
                    {
                        Subject = newSubject.Entity,
                        AcceptedAnswer = m.AcceptedAnswer,
                        Reading = m.Reading,
                        Type = m.Type,
                        Primary = m.Primary
                    }));
                }
            }

            context.SaveChanges();

            return summary;
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
