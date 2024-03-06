using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Interfaces
{
    public interface IWebApiService
    {
        Task<bool> Authorize(string username, string password);
    }
}
