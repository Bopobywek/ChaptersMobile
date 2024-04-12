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
        searchBarUser.TextChanged += OnTextChanged;
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

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void ButtonSub_Clicked(object sender, EventArgs e)
    {
        if (ButtonSub.BackgroundColor != Colors.LightGray)
        {
            return;
        }
        ButtonSub.BackgroundColor = ButtonNew.BackgroundColor;
        ButtonNew.BackgroundColor = Colors.LightGray;
        EventsSection.IsVisible = false;
        SubscriptionsSections.IsVisible = true;
        searchBarUser.IsVisible = true;
    }

    private void ButtonNew_Clicked(object sender, EventArgs e)
    {
        if (ButtonNew.BackgroundColor != Colors.LightGray)
        {
            return;
        }
        ButtonNew.BackgroundColor = ButtonSub.BackgroundColor;
        ButtonSub.BackgroundColor = Colors.LightGray;
        SubscriptionsSections.IsVisible = false;
        searchBarUser.IsVisible = false;
        MainThread.InvokeOnMainThreadAsync(_viewModel.UpdateSubscriptions);
        EventsSection.IsVisible = true;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs args)
    {
        if (searchBarUser.Text.Length > 0)
        {
            _viewModel.SearchUserCommand.Execute(searchBarUser.Text);
        }
        else
        {
            MainThread.InvokeOnMainThreadAsync(_viewModel.UpdateSubscriptions);
        }
    }
}