using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class BookPage : ContentPage
{
	public BookPage(BookViewModel viewModel)
	{
		InitializeComponent();
        ChaptersButton.BackgroundColor = Colors.LightGray;
        BindingContext = viewModel;
	}

    private void ReviewsButton_Clicked(object sender, EventArgs e)
    {
        if (ReviewsButton.BackgroundColor != Colors.LightGray)
        {
            return;
        }
        ReviewsButton.BackgroundColor = ChaptersButton.BackgroundColor;
        ChaptersButton.BackgroundColor = Colors.LightGray;
        ChaptersSection.IsVisible = false;
        ReviewsSection.IsVisible = true;
    }

    private void ChaptersButton_Clicked(object sender, EventArgs e)
    {
        if (ChaptersButton.BackgroundColor != Colors.LightGray)
        {
            return;
        }
        ChaptersButton.BackgroundColor = ReviewsButton.BackgroundColor;
        ReviewsButton.BackgroundColor = Colors.LightGray;
        ReviewsSection.IsVisible = false;
        ChaptersSection.IsVisible = true;
    }
}