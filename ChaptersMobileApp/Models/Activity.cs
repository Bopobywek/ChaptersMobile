using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Models
{
    public record Activity(
        int Id,
        int UserId,
        string Username,
        string Text,
        DateTimeOffset CreatedAt
    );
}
