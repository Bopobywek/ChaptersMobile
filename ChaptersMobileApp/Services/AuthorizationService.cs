using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Services
{
    public class AuthorizationService
    {
        public event Action AuthorizationChanged;

        protected virtual void OnAuthorizationChanged()
        {
            AuthorizationChanged?.Invoke();
        }

        public async Task Authorize(string username, string password)
        {
            await SecureStorage.SetAsync("username", username);
            await SecureStorage.SetAsync("password", password);
            OnAuthorizationChanged();
        }

        public void LogOut()
        {
            SecureStorage.Default.RemoveAll();
            OnAuthorizationChanged();
        }
    }
}
