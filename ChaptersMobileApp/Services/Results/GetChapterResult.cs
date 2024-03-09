using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public class GetChapterResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Number {  get; set; }
        public double Rating { get; set; }
        public bool IsRead { get; set; }
        public int UserRating { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
