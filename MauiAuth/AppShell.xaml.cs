using MauiAuth.Views;

namespace MauiAuth
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WeatherForecastPage), typeof(WeatherForecastPage));
        }
    }
}
