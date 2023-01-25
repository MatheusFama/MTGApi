using System.Text.Json.Serialization;

namespace MTGApi.Models.MTGApi
{
    public class Legality
    {
        public string Format { get; set; }

        [JsonPropertyName("legality")]
        public string legalityName { get; set; }
    }
}
