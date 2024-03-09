using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public record GetReviewResult
    (
        int Id,
        int AuthorId,
        string AuthorUsername,
        int AuthorBookRating,
        string Title,
        string Text,
        int Rating,
        int UserRating,
        DateTime CreatedAt
    );
}
