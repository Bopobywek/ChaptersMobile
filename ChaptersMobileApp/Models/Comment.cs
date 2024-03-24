using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Models
{
    public sealed record Comment(
        int Id,
        int AuthorId,
        string AuthorUsername,
        string Text,
        int Rating,
        int UserRating,
        int BookId,
        int ChapterId,
        string Relation,
        DateTimeOffset CreatedAt
    );
}
