using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class WriteReviewPage : ContentPage
{
    public WriteReviewPage(WriteReviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}