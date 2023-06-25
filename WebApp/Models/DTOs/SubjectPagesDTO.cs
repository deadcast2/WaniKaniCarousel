using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectPagesDTO
    {
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("next_url")]
        public Uri NextUrl { get; set; } = new Uri("https://wanikani.com");

        [JsonPropertyName("previous_url")]
        public Uri PreviousUrl { get; set; } = new Uri("https://wanikani.com");
    }
}
