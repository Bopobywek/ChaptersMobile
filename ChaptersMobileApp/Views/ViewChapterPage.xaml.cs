using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class ViewChapterPage : ContentPage
{
	public ViewChapterPage(ViewChapterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}