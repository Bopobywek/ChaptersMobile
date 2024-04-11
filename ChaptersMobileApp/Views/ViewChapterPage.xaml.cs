using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class ViewChapterPage : ContentPage
{
	public ViewChapterPage(ViewChapterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        commentEntry.IsEnabled = false;
        commentEntry.IsEnabled = true;
        commentEntry.Text = string.Empty;
    }
}