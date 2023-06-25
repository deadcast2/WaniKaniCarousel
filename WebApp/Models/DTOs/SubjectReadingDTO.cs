using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectReadingDTO
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("accepted_answer")]
        public bool AcceptedAnswer { get; set; }

        [JsonPropertyName("reading")]
        public string Reading { get; set; } = string.Empty;
    }
}
