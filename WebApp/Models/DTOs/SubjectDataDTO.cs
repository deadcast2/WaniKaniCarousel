using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectDataDTO
    {
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("hidden_at")]
        public DateTime? HiddenAt { get; set; }

        [JsonPropertyName("document_url")]
        public Uri DocumentUrl { get; set; } = new Uri("https://wanikani.com");

        [JsonPropertyName("characters")]
        public string Characters { get; set; } = string.Empty;

        [JsonPropertyName("meanings")]
        public List<SubjectMeaningDTO> Meanings { get; set; } = new();

        [JsonPropertyName("readings")]
        public List<SubjectReadingDTO> Readings { get; set; } = new();

        [JsonPropertyName("component_subject_ids")]
        public List<int> ComponentSubjectIds { get; set; } = new();

        [JsonPropertyName("amalgamation_subject_ids")]
        public List<int> AmalgamationSubjectIds { get; set; } = new();

        [JsonPropertyName("visually_similar_subject_ids")]
        public List<int> VisuallySimilarSubjectIds { get; set; } = new();

        [JsonPropertyName("meaning_mnemonic")]
        public string MeaningMnemonic { get; set; } = string.Empty;

        [JsonPropertyName("meaning_hint")]
        public string MeaningHint { get; set; } = string.Empty;

        [JsonPropertyName("reading_mnemonic")]
        public string ReadingMnemonic { get; set; } = string.Empty;

        [JsonPropertyName("reading_hint")]
        public string ReadingHint { get; set; } = string.Empty;

        [JsonPropertyName("lesson_position")]
        public int LessonPosition { get; set; }

        [JsonPropertyName("spaced_repetition_system_id")]
        public int SpacedRepetitionSystemId { get; set; }
    }
}
