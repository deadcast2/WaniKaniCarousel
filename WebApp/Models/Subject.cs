using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    [Index("RemoteId")]
    public class Subject
    {
        public int SubjectId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; } = new();

        public int Level { get; set; }

        public string Characters { get; set; } = string.Empty;

        public string? ImageData { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? HiddenAt { get; set; }

        public int SeenLock { get; set; }

        public int RemoteId { get; set; }

        public virtual ICollection<SubjectMeaning> Meanings { get; } = new HashSet<SubjectMeaning>();

        public virtual ICollection<SubjectReading> Readings { get; } = new HashSet<SubjectReading>();
    }
}