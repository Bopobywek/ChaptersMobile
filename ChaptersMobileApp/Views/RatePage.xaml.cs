using ChaptersMobileApp.ViewModels;

namespace ChaptersMobileApp.Views;

public partial class RatePage : ContentPage
{
	private bool update = false;
	private double[] values = new double[] { 0, 1, 2, 3, 4, 5 };
	public RatePage(RateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
		if (update)
		{
			update = false;
			return;
		}

		update = true;
		var result = values.MinBy(x => Math.Abs(x - e.NewValue));
		rateSlider.Value = result;
		rateValue.Text = result.ToString();
    }
}