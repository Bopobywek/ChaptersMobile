using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public class GetUsersResult
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("numberOfBooks")]
        public int NumberOfBooks { get; set; }
    }
}
