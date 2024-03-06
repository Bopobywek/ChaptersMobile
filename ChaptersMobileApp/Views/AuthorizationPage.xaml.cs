using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class AuthorizationPage : ContentPage
{
	public AuthorizationPage(AuthorizationViewModel viewModel)
    {
        InitializeComponent();
		BindingContext = viewModel;
        Shell.SetTabBarIsVisible(this, false);
    }
}