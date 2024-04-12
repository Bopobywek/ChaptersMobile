using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class RatingPage : ContentPage
{
    private readonly RatingViewModel viewModel;

    public RatingPage(RatingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		searchBar.TextChanged += OnTextChanged;
        this.viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		MainThread.InvokeOnMainThreadAsync(viewModel.UpdateBooks);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs args)
	{
		if (searchBar.Text.Length > 0)
		{
			viewModel.SearchBookCommand.Execute(searchBar.Text);
		}
		else
		{
			MainThread.InvokeOnMainThreadAsync(viewModel.UpdateBooks);
		}
	}


}