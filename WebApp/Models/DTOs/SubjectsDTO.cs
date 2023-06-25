using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectsDTO
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public Uri Url { get; set; } = new Uri("https://wanikani.com");

        [JsonPropertyName("pages")]
        public SubjectPagesDTO Pages { get; set; } = new();

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("data_updated_at")]
        public DateTime DataUpdatedAt { get; set; }

        [JsonPropertyName("data")]
        public List<SubjectDatumDTO> Data { get; set; } = new();
    }
}
