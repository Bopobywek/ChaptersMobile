using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public record GetSubscriptionsResult(
        int Id,
        int UserId,
        string Username,
        int NumberOfBooks
    );
}
