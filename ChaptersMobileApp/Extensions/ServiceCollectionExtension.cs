﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaptersMobileApp.Services;
using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.ViewModels;
using ChaptersMobileApp.Views;

namespace ChaptersMobileApp.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddTransient<AuthorizationPage>();
            services.AddTransient<RegistrationPage>();
            services.AddTransient<RatingPage>();
            services.AddSingleton<ReadingListPage>();
            services.AddSingleton<SubscriptionsPage>();
            services.AddTransient<ProfilePage>();
            services.AddTransient<RatingPage>();
            services.AddTransient<BookPage>();
            services.AddTransient<ViewBooksPage>();
            return services;
        }
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<RatingViewModel>();
            services.AddSingleton<ReadingListViewModel>();
            services.AddSingleton<SubscriptionsViewModel>();
            services.AddTransient<ViewBooksViewModel>();
            services.AddTransient<BookViewModel>();
            services.AddSingleton<AuthorizationService>();
            services.AddTransient<IAlertService, AlertService>();
            services.AddTransient<IWebApiService, WebApiService>();
            return services;
        }
    }
}
