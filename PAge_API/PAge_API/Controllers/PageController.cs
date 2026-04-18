using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PAge_API.Controllers
{
    [ApiController]
    [Route("api/page")]
    public class PageController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly string _pageToken;

        public PageController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient();
            _config = config;
            _baseUrl = config["Facebook:BaseUrl"] ?? "https://graph.facebook.com/v25.0";
            _pageToken = config["Facebook:PageAccessToken"] ?? "";
        }

        // GET /api/page/{pageId}
        [HttpGet("{pageId}")]
        public async Task<IActionResult> GetPage(string pageId)
        {
            var url = $"{_baseUrl}/{pageId}?fields=id,name,category,fan_count,picture&access_token={_pageToken}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // GET /api/page/{pageId}/posts
        [HttpGet("{pageId}/posts")]
        public async Task<IActionResult> GetPosts(string pageId)
        {
            var url = $"{_baseUrl}/{pageId}/posts?fields=id,message,created_time,full_picture&access_token={_pageToken}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // POST /api/page/{pageId}/posts
        [HttpPost("{pageId}/posts")]
        public async Task<IActionResult> CreatePost(string pageId, [FromBody] CreatePostRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Message))
                return BadRequest(new { message = "Message không được để trống" });

            var url = $"{_baseUrl}/{pageId}/feed";
            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("message", request.Message),
                new KeyValuePair<string, string>("access_token", _pageToken)
            });
            var response = await _httpClient.PostAsync(url, body);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // DELETE /api/page/post/{postId}
        [HttpDelete("post/{postId}")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var url = $"{_baseUrl}/{postId}?access_token={_pageToken}";
            var response = await _httpClient.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // GET /api/page/{pageId}/insights
        [HttpGet("{pageId}/insights")]
        public async Task<IActionResult> GetInsights(string pageId)
        {
            var url = $"{_baseUrl}/{pageId}/insights?metric=page_follows&period=day&access_token={_pageToken}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // GET /api/page/post/{postId}/comments
        [HttpGet("post/{postId}/comments")]
        public async Task<IActionResult> GetComments(string postId)
        {
            var url = $"{_baseUrl}/{postId}/comments?fields=id,message,from,created_time&access_token={_pageToken}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }

        // GET /api/page/post/{postId}/likes
        [HttpGet("post/{postId}/likes")]
        public async Task<IActionResult> GetLikes(string postId)
        {
            var url = $"{_baseUrl}/{postId}/likes?fields=id,name&access_token={_pageToken}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }
    }

    public class CreatePostRequest
    {
        public string? Message { get; set; }
    }
}