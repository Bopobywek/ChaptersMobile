using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public class GetCommentResult {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        [JsonPropertyName("authorUsername")]
        public string AuthorUsername { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int UserRating { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
