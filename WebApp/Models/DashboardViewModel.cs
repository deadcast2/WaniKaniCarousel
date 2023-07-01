namespace WebApp.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel() { }

        public DashboardViewModel(User user, Subject subject)
        {
            AvailableReviewsCount = user.AvailableReviewsCount;
            AvailableLessonsCount = user.AvailableLessonsCount;
            Username = user.Username;
            Level = user.Level;
            Characters = subject.Characters;
            ImageData = subject.ImageData;
            Meaning = subject.Meanings.FirstOrDefault(m => m.Primary)?.Meaning;
            Reading = subject.Readings.FirstOrDefault(m => m.Primary)?.Reading;
        }

        public string Username { get; set; }

        public string Characters { get; set; } = "No subjects downloaded yet :(";

        public string? ImageData { get; set; }
        
        public string? Meaning { get; }

        public string? Reading { get; }

        public int Level { get; }

        public int AvailableReviewsCount { get; }

        public int AvailableLessonsCount { get; }

        public bool HasImage => !string.IsNullOrWhiteSpace(ImageData);
    }
}
