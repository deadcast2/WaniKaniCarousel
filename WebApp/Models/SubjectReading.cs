namespace WebApp.Models
{
    public class SubjectReading
    {
        public int SubjectReadingId { get; set; }

        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; } = new();

        public string Type { get; set; } = string.Empty;

        public bool Primary { get; set; }

        public bool AcceptedAnswer { get; set; }

        public string Reading { get; set; } = string.Empty;
    }
}