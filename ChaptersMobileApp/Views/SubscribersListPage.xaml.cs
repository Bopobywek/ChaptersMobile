using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class SubscribersListPage : ContentPage
{
	public SubscribersListPage(SubscribersListViewModel subscribersListViewModel)
	{
		InitializeComponent();
		BindingContext = subscribersListViewModel;
	}
}