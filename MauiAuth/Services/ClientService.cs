﻿
using MauiAuth.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MauiAuth.Services
{
    public class ClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task Register(RegisterModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/register", model);
            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "successfully Register", "Ok");
            }
            await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok");
        }
        public async Task Login(LoginModel model)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
            if (result.IsSuccessStatusCode)
            {
                var serializeResponse = JsonSerializer.Serialize(new LoginResponse() { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken, UserName = model.Email });
                await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
            }
        }
        public async Task<WeatherForecast[]> GetWeatherForeCastData()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null) return null!;
            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken!;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");
            return result!;
        }
    }
}
