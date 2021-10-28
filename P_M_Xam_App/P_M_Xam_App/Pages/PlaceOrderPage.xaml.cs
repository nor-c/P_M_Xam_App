using P_M_Xam_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using P_M_Xam_App.ApiServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P_M_Xam_App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        public ObservableCollection<Currency> CurrencyCollection;
        public ObservableCollection<Product> ProductsCollection;
        private int _currencyId;
        private int _areaId;
        private int _productId;
        private double _price;
        private string _desc;
        private DateTime _date;
        private string _currencyType;


        public PlaceOrderPage(int areaId, string areaName, int currencyId)
        {
            InitializeComponent();
            LblAreaName.Text = areaName;
            _areaId = areaId;
            CurrencyCollection = new ObservableCollection<Currency>();
            ProductsCollection = new ObservableCollection<Product>();
            GetCurrencyId(areaId);
            GetProduct(areaId);


        }

        private async void GetProduct(int areaId)
        {
            var products = await ApiService.GetProductList(areaId);

            foreach (var product in products)
            {
                _productId = product.ProductID;
                ProductsCollection.Add(product);
            }

            CvProducts.ItemsSource = ProductsCollection;
        }

        private async void GetCurrencyId(int areaId)
        {
            var auth = Preferences.Get("authId", string.Empty);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-credentials-auth", Preferences.Get("token", string.Empty));
            httpClient.DefaultRequestHeaders.Add("x-access-ID", auth);
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Reservation/Area/Details/" + areaId);
            JObject r = JObject.Parse(response);
            _currencyId = (int)r["details"]["currencyID"];

            GetCurrencyType();
        }

        private async void GetCurrencyType()
        {
            var products = await ApiService.GetCurrencyType();
            foreach (var product in products)
            {
                CurrencyCollection.Add(product);
                if (product.TypeID == _currencyId)
                {
                    _currencyType = product.Type;
                    LblCurrencyID.Text = _currencyType;
                }
            }
        }

        private void CvProducts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Product;
            if (currentSelection == null)
            {
                return;
            }

            _date = currentSelection.From;
            _desc = currentSelection.ProductName;
            _price = currentSelection.Price;
            LblTotalPrice.Text = _price.ToString();
        }

        private async void BtnPlaceOrder_OnClicked(object sender, EventArgs e)
        {
            var order = new ProductsSelect();
            order.AreaID = _areaId;
            order.Quantity = 1;
            order.ProductID = _productId;
            order.From = _date;
            order.PromoCode = null;
            order.References = new List<string>
            {
                "sample string",
                "sample string"
            };
            var response = await ApiService.PlaceOrder(order);
            Application.Current.MainPage = new NavigationPage(new ReservationSummaryPage(_desc, _date, _price, _currencyType, _areaId, _productId));

        }
    }
}