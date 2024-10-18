using MauiAuth.ViewModel;

namespace MauiAuth.Views;

public partial class WeatherForecastPage : ContentPage
{
	public WeatherForecastPage(WeatherForecastViewModel weatherForecastViewModel)
	{
		InitializeComponent();
		BindingContext = weatherForecastViewModel;
	}
}