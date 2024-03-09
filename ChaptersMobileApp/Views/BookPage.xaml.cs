using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class BookPage : ContentPage
{
	public BookPage(BookViewModel viewModel)
	{
		InitializeComponent();
        ChaptersButton.BackgroundColor = Colors.Gray;
        BindingContext = viewModel;
	}

    private void ReviewsButton_Clicked(object sender, EventArgs e)
    {
        if (ReviewsButton.BackgroundColor != Colors.Gray)
        {
            return;
        }
        ReviewsButton.BackgroundColor = ChaptersButton.BackgroundColor;
        ChaptersButton.BackgroundColor = Colors.Gray;
        ChaptersSection.IsVisible = false;
        ReviewsSection.IsVisible = true;
    }

    private void ChaptersButton_Clicked(object sender, EventArgs e)
    {
        if (ChaptersButton.BackgroundColor != Colors.Gray)
        {
            return;
        }
        ChaptersButton.BackgroundColor = ReviewsButton.BackgroundColor;
        ReviewsButton.BackgroundColor = Colors.Gray;
        ReviewsSection.IsVisible = false;
        ChaptersSection.IsVisible = true;
    }
}