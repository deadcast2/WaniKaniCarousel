namespace WebApp.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel() { }

        public DashboardViewModel(Subject subject)
        {
            Characters = subject.Characters;
            ImageData = subject.ImageData;
            Meaning = subject.Meanings.FirstOrDefault(m => m.Primary)?.Meaning;
            Reading = subject.Readings.FirstOrDefault(m => m.Primary)?.Reading;
        }

        public string Characters { get; set; } = "No subjects downloaded yet :(";

        public string? ImageData { get; set; }
        
        public string? Meaning { get; }

        public string? Reading { get; }

        public bool HasImage => !string.IsNullOrWhiteSpace(ImageData);
    }
}
