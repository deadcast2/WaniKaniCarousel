using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SummaryDataDTO
    {
        [JsonPropertyName("lessons")]
        public List<SummaryLessonDTO> Lessons { get; set; } = new();

        [JsonPropertyName("next_reviews_at")]
        public DateTime NextReviewsAt { get; set; }

        [JsonPropertyName("reviews")]
        public List<SummaryLessonDTO> Reviews { get; set; } = new();
    }
}
