using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectMeaningDTO
    {
        [JsonPropertyName("meaning")]
        public string Meaning { get; set; } = string.Empty;

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("accepted_answer")]
        public bool AcceptedAnswer { get; set; }
    }
}
