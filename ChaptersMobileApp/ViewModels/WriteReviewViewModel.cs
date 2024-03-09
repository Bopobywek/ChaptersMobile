using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class WriteReviewViewModel : ObservableValidator, IQueryAttributable
    {
        [ObservableProperty]
        private string _text;

        [ObservableProperty]
        private string _title;

        private int _bookId;
        private readonly IWebApiService _webApiService;

        public WriteReviewViewModel(IWebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        [RelayCommand]
        public async Task WriteReview()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "return", "ok" }
            };

            await _webApiService.PostReview(_bookId, Title, Text);
            await Shell.Current.GoToAsync("..", navigationParameter);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _bookId = (int)query["bookId"];

            query.Clear();
        }
    }
}
