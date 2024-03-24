using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class ActivityPage : ContentPage
{
	public ActivityPage(ActivityViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}