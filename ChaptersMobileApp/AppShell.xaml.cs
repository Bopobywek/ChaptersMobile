using ChaptersMobileApp.Views;

namespace ChaptersMobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("auth", typeof(AuthorizationPage));
            InitializeComponent();
        }
    }
}
