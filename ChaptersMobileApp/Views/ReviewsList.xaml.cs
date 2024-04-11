using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class ReviewsList : ContentPage
{
	public ReviewsList(ReviewsListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}