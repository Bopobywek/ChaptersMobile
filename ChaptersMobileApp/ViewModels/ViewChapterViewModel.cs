﻿using ChaptersMobileApp.Models;
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
    public partial class ViewChapterViewModel : ObservableValidator, IQueryAttributable
    {
        private readonly IWebApiService _webApiService;
        private int _chapterId;
        private string? username;

        [ObservableProperty]
        private string _title;
        public ObservableCollection<Comment> CommentList { get; } = new();

        [ObservableProperty]
        public string _commentText;

        public ViewChapterViewModel(IWebApiService webApiService)
        {
            _webApiService = webApiService;
        }

        [RelayCommand]
        public async Task LikeComment(Comment comment)
        {
            await _webApiService.RateComment(comment.Id, true);
            await Update();
        }

        [RelayCommand]
        public async Task DislikeComment(Comment comment)
        {
            await _webApiService.RateComment(comment.Id, false);
            await Update();
        }

        [RelayCommand]
        public async Task Return()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "return", true },
                { "username", username }
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }

        [RelayCommand]
        public async Task SendComment()
        {
            if (await _webApiService.PostComment(_chapterId, CommentText))
            {
                var comments = await _webApiService.GetComments(_chapterId);

                foreach (var comment in comments)
                {
                    if (!CommentList.Where(item => item.Id == comment.Id).Any())
                        CommentList.Add(
                            new Comment(comment.Id,
                                comment.AuthorId,
                                comment.AuthorUsername,
                                comment.Text,
                                comment.Rating,
                                comment.UserRating,
                                0,
                                0,
                                null,
                                comment.CreatedAt
                            )
                        );
                }
            }
        }

        private async Task Update()
        {
            var comments = await _webApiService.GetComments(_chapterId);
            CommentList.Clear();
            foreach (var comment in comments)
            {
                CommentList.Add(
                    new Comment(comment.Id,
                        comment.AuthorId,
                        comment.AuthorUsername,
                        comment.Text,
                        comment.Rating,
                        comment.UserRating,
                        0,
                        0,
                        null,
                        comment.CreatedAt
                    )
                );
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("username", out var usernameObj))
            {
                username = (string)usernameObj;   
            }

            var chapterId = (int)query["chapterId"];
            if (query.TryGetValue("title", out var titleObj))
            {
                Title = (string)titleObj;
            }
            else
            {
                var bookId = (int)query["bookId"];
                var chapters = await _webApiService.GetChapters(bookId);
                var chapter = chapters.Single(x => x.Id == chapterId);
                Title = chapter.Title;
            }
            _chapterId = chapterId;

            await Update();

            query.Clear();
        }
    }
}
