using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    [Index("Username")]
    public class User
    {
        public int UserId { get; set; }

        public int Level { get; set; }

        public string Username { get; set; } = string.Empty;

        public DateTime StartedAt { get; set; }

        public int AvailableReviewsCount { get; set; }

        public int AvailableLessonsCount { get; set; }
    }
}