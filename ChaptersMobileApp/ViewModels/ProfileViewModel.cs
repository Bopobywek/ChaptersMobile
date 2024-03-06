using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.ViewModels
{
    public partial class ProfileViewModel : AuthorizedViewModel
    {
        [ObservableProperty]
        private string _username;


        public ProfileViewModel()
        { 
            UpdateCommand = new RelayCommand(Update);
        }

        protected override void Update()
        { 
            base.Update();

            var username = SecureStorage.GetAsync("username").Result;
            Username = username;
        }

    }
}
