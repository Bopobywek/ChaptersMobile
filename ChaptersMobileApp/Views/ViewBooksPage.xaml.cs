using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class ViewBooksPage : ContentPage
{
	public ViewBooksPage(ViewBooksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}