using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services.Interfaces
{
    public interface IAlertService
    {
        Task ShowSnackbar(string text, Color color = default!);
        Task DisplayAlert(string text);
    }
}
