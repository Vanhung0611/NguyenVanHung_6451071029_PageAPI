namespace PAge_API.Models
{
    public class PageInfoModel
    {
        public string? PageId { get; set; }
        public string? PageName { get; set; }
        public string? PageAccessToken { get; set; }
        public string? Category { get; set; }
    }

    public class TokenRequest
    {
        public string? UserAccessToken { get; set; }
    }

    public class TokenResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<PageInfoModel>? Pages { get; set; }
    }
}