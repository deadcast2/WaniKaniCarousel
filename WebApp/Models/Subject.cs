using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    [Index("RemoteId")]
    public class Subject
    {
        public int SubjectId { get; set; }

        public int Level { get; set; }

        public string Characters { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? HiddenAt { get; set; }

        public int SeenLock { get; set; }

        public int RemoteId { get; set; }

        public List<SubjectMeaning> Meanings { get; } = new();

        public List<SubjectReading> Readings { get; } = new();
    }
}