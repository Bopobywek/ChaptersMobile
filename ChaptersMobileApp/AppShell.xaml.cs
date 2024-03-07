using ChaptersMobileApp.Views;

namespace ChaptersMobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("auth", typeof(AuthorizationPage));
            Routing.RegisterRoute("register", typeof(RegistrationPage));
            Routing.RegisterRoute("viewBooks", typeof(ViewBooksPage));
            Routing.RegisterRoute("bookView", typeof(BookPage));
            InitializeComponent();
        }
    }
}
