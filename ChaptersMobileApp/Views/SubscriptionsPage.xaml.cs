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

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void ButtonSub_Clicked(object sender, EventArgs e)
    {
        if (ButtonSub.BackgroundColor != Colors.Gray)
        {
            return;
        }
        ButtonSub.BackgroundColor = ButtonNew.BackgroundColor;
        ButtonNew.BackgroundColor = Colors.Gray;
        SubscriptionsSections.IsVisible = true;
    }

    private void ButtonNew_Clicked(object sender, EventArgs e)
    {
        if (ButtonNew.BackgroundColor != Colors.Gray)
        {
            return;
        }
        ButtonNew.BackgroundColor = ButtonSub.BackgroundColor;
        ButtonSub.BackgroundColor = Colors.Gray;
        SubscriptionsSections.IsVisible = false;
    }
}