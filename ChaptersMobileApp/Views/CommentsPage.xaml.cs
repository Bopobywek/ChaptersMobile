using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class CommentsPage : ContentPage
{
	public CommentsPage(CommentsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}