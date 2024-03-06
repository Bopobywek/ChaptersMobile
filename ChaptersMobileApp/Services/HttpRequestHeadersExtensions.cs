using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services
{
    public static class HttpRequestHeadersExtensions
    {
        public static void AddBasicAuthHeader(this HttpRequestHeaders headers, string username, string password)
        {
            string credentials = $"{username}:{password}";
            byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
            string base64Credentials = Convert.ToBase64String(credentialsBytes);
            headers.Add("Authorization", $"Basic {base64Credentials}");
        }
    }
}
