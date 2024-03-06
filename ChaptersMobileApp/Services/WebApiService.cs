using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper,
                WriteIndented = true
            };
        }
        public async Task<bool> Authorize(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_webApiSettings.Url}/api/test");
            string credentials = $"{username}:{password}";
            byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
            string base64Credentials = Convert.ToBase64String(credentialsBytes);
            request.Headers.Add("Authorization", $"Basic {base64Credentials}");

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
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
    }
}
