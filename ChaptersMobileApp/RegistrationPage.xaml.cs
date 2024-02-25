using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationViewModel registrationViewModel)
	{
		InitializeComponent();
		BindingContext = registrationViewModel;

    }
}