namespace WebApp.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel() { }

        public DashboardViewModel(Subject subject)
        {
            Characters = subject.Characters;
            ImageData = subject.ImageData;
        }

        public string Characters { get; set; } = "No subject downloaded";

        public string? ImageData { get; set; }

        public bool HasImage => !string.IsNullOrWhiteSpace(ImageData);
    }
}
