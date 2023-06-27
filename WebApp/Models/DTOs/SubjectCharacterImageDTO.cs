using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectCharacterImageDTO
    {
        [JsonPropertyName("url")]
        public Uri Url { get; set; } = new Uri("https://wanikani.com");

        [JsonPropertyName("metadata")]
        public SubjectMetadataDTO Metadata { get; set; } = new();

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; } = string.Empty;
    }
}
