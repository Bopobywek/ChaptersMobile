using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class ReadingListPage : ContentPage
{
    private readonly ReadingListViewModel _viewModel;

    public ReadingListPage(ReadingListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
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