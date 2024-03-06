﻿using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Services.Results;
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
