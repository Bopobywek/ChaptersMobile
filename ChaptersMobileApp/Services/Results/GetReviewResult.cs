using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public class GetReviewResult
    {
        public int Id { get; set; }
        [JsonPropertyName("authorId")]

        public int AuthorId { get; set; }
    
        [JsonPropertyName("authorUsername")]
        public string AuthorUsername { get; set; }
        public int AuthorBookRating { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        [JsonPropertyName("userRating")]
        public int UserRating { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    };
}
