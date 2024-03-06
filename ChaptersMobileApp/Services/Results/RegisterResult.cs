using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Results
{
    public record RegisterResult(bool IsSuccessStatusCode, HttpStatusCode StatusCode, string Message);
}
