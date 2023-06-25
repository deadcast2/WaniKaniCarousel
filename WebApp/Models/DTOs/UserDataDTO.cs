using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class UserDataDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("profile_url")]
        public Uri ProfileUrl { get; set; } = new Uri("https://wanikani.com");

        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }

        [JsonPropertyName("current_vacation_started_at")]
        public DateTime? CurrentVacationStartedAt { get; set; }

        [JsonPropertyName("subscription")]
        public UserSubscriptionDTO Subscription { get; set; } = new();

        [JsonPropertyName("preferences")]
        public UserPreferencesDTO Preferences { get; set; } = new();
    }
}
