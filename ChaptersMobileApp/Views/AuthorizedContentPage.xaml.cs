using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class AuthorizedContentPage : ContentPage
{
    private readonly AuthorizedViewModel viewModel;

    public AuthorizedContentPage(AuthorizedViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);


    }
}