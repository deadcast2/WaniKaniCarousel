namespace WebApp.Models
{
    public class SubjectMeaning
    {
        public int SubjectMeaningId { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; } = new();

        public string Meaning { get; set; } = string.Empty;

        public bool Primary { get; set; }

        public bool AcceptedAnswer { get; set; }
    }
}