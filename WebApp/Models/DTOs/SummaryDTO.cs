using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public class SummaryDTO
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public Uri Url { get; set; } = new("https://wanikani.com");

        [JsonPropertyName("data_updated_at")]
        public DateTime DataUpdatedAt { get; set; }

        [JsonPropertyName("data")]
        public SummaryDataDTO Data { get; set; } = new();
    }
}
