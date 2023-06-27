using System.Text.Json.Serialization;

namespace WebApp.Models.DTOs
{
    public partial class SubjectMetadataDTO
    {
        [JsonPropertyName("inline_styles")]
        public bool InlineStyles { get; set; }
    }
}
