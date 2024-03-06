using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}