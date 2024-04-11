using ChaptersMobileApp.Models;
using ChaptersMobileApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class ReviewsListViewModel : ObservableValidator, IQueryAttributable
    {
        private readonly IWebApiService webApiService;

        [ObservableProperty]
        private Review _selectedReview = null;
        private string? username = null;

        public ObservableCollection<Review> Reviews { get; } = new();
        public ReviewsListViewModel(IWebApiService webApiService)
        {
            this.webApiService = webApiService;
        }

        [RelayCommand]
        public async Task Return()
        {
            var navigationParameter = new Dictionary<string, object>
            {
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("username", out var obj))
            {
                username = (string)obj;
            }

            query.Clear();

            var reviews = await webApiService.GetReviewsByUser(username);
            Reviews.Clear();
            foreach (var review in reviews)
            {
                Reviews.Add(
                    new()
                    {
                        Id = review.Id,
                        Title = review.Title.Trim(),
                        Text = review.Text,
                        AuthorBookRating = review.AuthorBookRating,
                        Rating = review.Rating,
                        UserRating = review.UserRating,
                        AuthorId = review.AuthorId,
                        AuthorUsername = review.AuthorUsername
                    }
                );
            }

        }
    }
}
