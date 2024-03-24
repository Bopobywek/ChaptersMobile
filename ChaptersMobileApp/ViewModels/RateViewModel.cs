using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class RateViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private double _rating = 0;
        private int _itemId = 0;

        [RelayCommand]
        public async Task Rate()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "userRating", Rating },
                { "itemId", _itemId }
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var currentRating = query["currentRating"];
            _itemId = (int)query["itemId"];
            Rating = (double)currentRating;
            query.Clear();
        }
    }
}
