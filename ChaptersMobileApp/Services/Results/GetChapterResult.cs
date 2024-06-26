﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public class GetChapterResult
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        [JsonPropertyName("isRead")]
        public bool IsRead { get; set; }
        [JsonPropertyName("userRating")]
        public int UserRating { get; set; }
    }
}
