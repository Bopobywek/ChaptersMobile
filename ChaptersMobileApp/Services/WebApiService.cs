using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Services.Results;
using ChaptersMobileApp.Settings;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Web;
using GetSubscriptionsResult = ChaptersMobileApp.Models.GetSubscriptionsResult;

namespace ChaptersMobileApp.Services
{
    public class WebApiService : IWebApiService
    {
        private readonly HttpClient _httpClient = new(GetInsecureHandler());
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly WebApiSettings _webApiSettings;

        public WebApiService(IOptions<WebApiSettings> options)
        {
            _webApiSettings = options.Value;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = true
            };
        }

        public async Task<bool> Authorize(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/test");
            request.Headers.AddBasicAuthHeader(username, password);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<RegisterResult> Register(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/users");
            var item = new { Username = username, Password = password };
            string json = JsonSerializer.Serialize(item, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return new RegisterResult(response.IsSuccessStatusCode, response.StatusCode, response.Content.ToString());
        }

        public async Task<List<GetBooksResult>> GetBooks(BookStatus? bookStatus = null, string? username = null)
        {
            HttpRequestMessage request;
            if (bookStatus is not null)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["bookStatus"] = bookStatus.ToString();
                if (username is not null)
                {
                    query["name"] = username;
                }
                string queryString = query.ToString();
                request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/books?{queryString}");
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/books");
            }

            var name = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (name is not null)
            {
                request.Headers.AddBasicAuthHeader(name, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetBooksResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetBooksResult>>(str, _serializerOptions) ?? new List<GetBooksResult>();
        }

        public async Task<GetBooksResult?> GetBook(int bookId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/books/{bookId}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetBooksResult>(str, _serializerOptions);
        }

        public async Task<bool> ChangeBookStatus(int bookId, BookStatus bookStatus)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/books/{bookId}/status")
            {
                Content = new StringContent(((int)bookStatus).ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)
            };
            
            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RateBook(int bookId, int rating)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/books/{bookId}/rating")
            {
                Content = new StringContent(rating.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)
            };
            
            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetBooksResult>> SearchBooks(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/books?q={query}");
            
            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetBooksResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetBooksResult>>(str, _serializerOptions) ?? new List<GetBooksResult>();
        }


        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<List<GetChapterResult>> GetChapters(int bookId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/chapters/{bookId}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetChapterResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetChapterResult>>(str, _serializerOptions);
        }

        public async Task<bool> MarkChapter(int chapterId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/chapters/{chapterId}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            request.Headers.AddBasicAuthHeader(username, password);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RateChapter(int chapterId, int rating)
        {
            var request =
                new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/chapters/{chapterId}/rating")
                {
                    Content = new StringContent(rating.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
            
            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetReviewResult>> GetReviews(int bookId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/reviews/{bookId}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetReviewResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetReviewResult>>(str, _serializerOptions);
        }

        public async Task<bool> UnmarkChapter(int chapterId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_webApiSettings.Url}/api/chapters/{chapterId}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            request.Headers.AddBasicAuthHeader(username, password);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PostReview(int bookId, string title, string text)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/reviews");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            StringContent content = new StringContent($"\"bookId\": {bookId}, \"title\": \"{title}\", \"text\": \"{text}\"", Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetUserReviewResult>> GetReviewsByUser(string author)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/reviews/user?author={author}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetUserReviewResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetUserReviewResult>>(str, _serializerOptions);
        }

        public async Task<bool> RateReview(int reviewId, bool rating)
        {
            var request =
                new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/reviews/{reviewId}")
                {
                    Content = new StringContent(rating ? "true" : "false", Encoding.UTF8, MediaTypeNames.Application.Json)
                };
            
            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetCommentResult>> GetComments(int chapterId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/comments/{chapterId}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetCommentResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetCommentResult>>(str, _serializerOptions);
        }

        public async Task<bool> PostComment(int chapterId, string text)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/comments");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            StringContent content = new StringContent($"{{ \"chapterId\": {chapterId}, \"text\": \"{text}\" }}", Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetUserCommentResult>> GetCommentsByUser(string author)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/comments/user?author={author}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetUserCommentResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetUserCommentResult>>(str, _serializerOptions);
        }

        public async Task<bool> RateComment(int commentId, bool rating)
        {
            var request =
                new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/comments/{commentId}")
                {
                    Content = new StringContent(rating ? "true" : "false", Encoding.UTF8, MediaTypeNames.Application.Json)
                };
            
            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Subscribe(int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/subscribers")
            {
                Content = new StringContent(userId.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var name = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (name is not null)
            {
                request.Headers.AddBasicAuthHeader(name, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Unsubscribe(int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_webApiSettings.Url}/api/subscribers")
            {
                Content = new StringContent(userId.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            var name = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (name is not null)
            {
                request.Headers.AddBasicAuthHeader(name, password);
            }

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<GetUserActivityResult>> GetUserActivities(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/activities/{username}");

            var name = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (name is not null)
            {
                request.Headers.AddBasicAuthHeader(name, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetUserActivityResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetUserActivityResult>>(str, _serializerOptions);
        }

        public async Task<List<GetUserActivityResult>> GetSubscriptionsActivities()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/activities");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetUserActivityResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetUserActivityResult>>(str, _serializerOptions);
        }

        public async Task<List<GetSubscriptionsResult>> GetSubscriptions(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_webApiSettings.Url}/api/subscribers/{username}");

            var name = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (name is not null)
            {
                request.Headers.AddBasicAuthHeader(name, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetSubscriptionsResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetSubscriptionsResult>>(str, _serializerOptions);
        }

        public async Task<List<GetUsersResult>> SearchUsers(string q)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/subscribers/search?q={q}");

            var username = await SecureStorage.Default.GetAsync("username");
            var password = await SecureStorage.Default.GetAsync("password");

            if (username is not null)
            {
                request.Headers.AddBasicAuthHeader(username, password);
            }

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<GetUsersResult>();
            }

            var str = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GetUsersResult>>(str, _serializerOptions) ?? new List<GetUsersResult>();
        }
    }
}