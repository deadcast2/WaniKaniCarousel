using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class UserDTO
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public Uri Url { get; set; } = new Uri("https://wanikani.com");

        [JsonPropertyName("data_updated_at")]
        public DateTime DataUpdatedAt { get; set; }

        [JsonPropertyName("data")]
        public UserDataDTO Data { get; set; } = new();
    }
}
