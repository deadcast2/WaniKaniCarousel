using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SummaryLessonDTO
    {
        [JsonPropertyName("available_at")]
        public DateTime AvailableAt { get; set; }

        [JsonPropertyName("subject_ids")]
        public List<int> SubjectIds { get; set; } = new();
    }
}
