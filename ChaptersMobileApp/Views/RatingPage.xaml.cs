using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class RatingPage : ContentPage
{
	public RatingPage(RatingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}