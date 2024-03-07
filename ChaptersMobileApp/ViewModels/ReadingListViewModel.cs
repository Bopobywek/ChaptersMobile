using ChaptersMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public class ReadingListViewModel : AuthorizedViewModel
    {
        public ReadingListViewModel(AuthorizationService authorizationService) : base(authorizationService)
        {
            authorizationService.AuthorizationChanged += base.Update;
        }
    }
}
