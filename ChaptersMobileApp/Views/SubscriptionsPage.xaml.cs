using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class SubscriptionsPage : ContentPage
{
    private readonly SubscriptionsViewModel _viewModel;

    public SubscriptionsPage(SubscriptionsViewModel subscriptionsViewModel)
	{
		InitializeComponent();
        _viewModel = subscriptionsViewModel;
        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.UpdateCommand.Execute(this);
    }

    public void Update()
    {
        _viewModel.UpdateCommand.Execute(this);
    }
}