using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class UserSubscriptionDTO
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("max_level_granted")]
        public int MaxLevelGranted { get; set; }

        [JsonPropertyName("period_ends_at")]
        public DateTime PeriodEndsAt { get; set; }
    }
}
