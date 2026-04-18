using System.Text.Json.Serialization;

namespace PAge_API.Models
{
    public class FacebookPagesResponse
    {
        [JsonPropertyName("data")]
        public List<PageData>? Data { get; set; }

        [JsonPropertyName("error")]
        public FacebookError? Error { get; set; }
    }

    public class PageData
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }
    }

    public class FacebookError
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}