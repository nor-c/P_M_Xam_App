using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using P_M_Xam_App.Models;
using Xamarin.Essentials;

namespace P_M_Xam_App.ApiServices
{
    public class ApiService
    {
        public static async Task<bool> Login(string authId, string credentials)
        {
            var login = new Login()
            {
                AuthID = authId,
                Credentials = credentials
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "Management/Login", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiToken>(jsonResult);
            Preferences.Set("token", result.Token);
            Preferences.Set("authId", result.AuthId);
            Preferences.Set("expiryDate", result.ExpiryDate);
            return true;
        }

        public static async Task<List<Provider>> GetProviders()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Reservation/Providers/");

            var result = JsonDocument.Parse(response).RootElement.GetProperty("providers").ToString();


            return JsonConvert.DeserializeObject<List<Provider>>(result);
        }

        public static async Task<List<AreaDetails>> GetArea(int areaId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Reservation/Areas/" + areaId);
            var result = JsonDocument.Parse(response).RootElement.GetProperty("areas").ToString();
            return JsonConvert.DeserializeObject<List<AreaDetails>>(result);
        }
        public static async Task<List<Product>> GetProductList(int areaId)
        {

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Reservation/Products/" + areaId);
            var result = JsonDocument.Parse(response).RootElement.GetProperty("products").ToString();


            return JsonConvert.DeserializeObject<List<Product>>(result);
        }
        public static async Task<List<Currency>> GetCurrencyType()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Reservation/CurrencyTypes");
            var result = JsonDocument.Parse(response).RootElement.GetProperty("currencies").ToString();
            return JsonConvert.DeserializeObject<List<Currency>>(result);
        }

        public static async Task<OrderResponse> PlaceOrder(ProductsSelect order)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "Reservation/Products/Select", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }
        public static async Task<OrderResponse> PlaceOrderSummary(ProductsSelect order)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "Reservation/Products/Confirm", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }
        public static async Task<OrderResponse> NewCard(ProductsSelect order)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "Reservation/Products//Payment", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public static async Task<ProductDetails> GetOrderProductDetails(int orderId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", Preferences.Get("authId", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Completed/Details/" + orderId);
            var result = JsonDocument.Parse(response).RootElement.GetProperty("details").ToString();
            return JsonConvert.DeserializeObject<ProductDetails>(result);
        }
    }

}
