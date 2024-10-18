using CommunityToolkit.Mvvm.ComponentModel;
using MauiAuth.Models;
using MauiAuth.Services;
using System.Collections.ObjectModel;
namespace MauiAuth.ViewModel
{
    public partial class WeatherForecastViewModel : ObservableObject
    {
        private readonly ClientService clientService;
        public ObservableCollection<WeatherForecast> WeatherForecasts { get; set; } = [];
        public WeatherForecastViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            LoadWeatherForecastData();
        }
        public async void LoadWeatherForecastData()
        {
            var response = await clientService.GetWeatherForeCastData();
            WeatherForecasts?.Clear();
            if (response.Any())
                foreach(var weatherForecast in response)
                    WeatherForecasts!.Add(weatherForecast);
        }
    }
}
