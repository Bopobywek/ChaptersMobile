using ChaptersMobileApp.Views;
using System.Globalization;

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
            Routing.RegisterRoute("writeReview", typeof(WriteReviewPage));
            Routing.RegisterRoute("viewChapter", typeof(ViewChapterPage));
            Routing.RegisterRoute("comments", typeof(CommentsPage));
            Routing.RegisterRoute("activities", typeof(ActivityPage));
            Routing.RegisterRoute("rate", typeof(RatePage));
            Routing.RegisterRoute("profile", typeof(ProfilePage));
            Routing.RegisterRoute("reviewsList", typeof(ReviewsList));
            Routing.RegisterRoute("subsList", typeof(SubscribersListPage));
            CultureInfo russianCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = russianCulture;
            Thread.CurrentThread.CurrentUICulture = russianCulture;
            InitializeComponent();
        }
    }
}
