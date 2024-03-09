using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        [JsonProperty("isRead")]
        public bool IsRead { get; set; }
        public int UserRating { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
