using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    [Index(nameof(Username)), Index(nameof(ApiKey))]
    public class User
    {
        public int UserId { get; set; }

        public int Level { get; set; }

        public string Username { get; set; } = string.Empty;

        public DateTime StartedAt { get; set; }

        public int AvailableReviewsCount { get; set; }

        public int AvailableLessonsCount { get; set; }

        public string ApiKey { get; set; } = string.Empty;

        public virtual ICollection<Subject> Subjects { get; } = new HashSet<Subject>();
    }
}