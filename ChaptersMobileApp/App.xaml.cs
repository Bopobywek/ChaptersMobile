namespace ChaptersMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RegistrationPage(new ViewModels.RegistrationViewModel(new HttpClient())));
        }
    }
}
