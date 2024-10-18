

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAuth.Models;
using MauiAuth.Services;
using MauiAuth.Views;
using System.Text.Json;

namespace MauiAuth.ViewModel
{
    public partial class MainPageViewModel:ObservableObject
    {
        [ObservableProperty]
        private RegisterModel registerModel;
        [ObservableProperty]
        private LoginModel loginModel;
        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private bool isAuthenticate;
        private readonly ClientService clientService;

        public MainPageViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            RegisterModel = new RegisterModel();
            LoginModel = new LoginModel();
            IsAuthenticate = false;
            GetUserNameFromSecuredStorage();
        }

       

        [RelayCommand]
        private async Task Register()
        {
            await clientService.Register(RegisterModel);
        }
        [RelayCommand]
        private async Task Login()
        {
            await clientService.Login(LoginModel);
            GetUserNameFromSecuredStorage();

        }
        [RelayCommand]
        private async Task Logout()
        {
            SecureStorage.Default.Remove("Authentication");
            IsAuthenticate = false;
            UserName = "Guest";
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private async Task GoToWeatherForecast()
        {
            await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
            GetUserNameFromSecuredStorage();

        }


        private async void GetUserNameFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(UserName) && userName != "Guest")
            {
                IsAuthenticate = true; return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if(serializedLoginResponseInStorage != null)
            {
                UserName = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.UserName!;
                IsAuthenticate = true;
                return;
            }
            UserName = "Guest";
            IsAuthenticate = false;
        }
    }
}
