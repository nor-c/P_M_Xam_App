using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_M_Xam_App.ApiServices;
using P_M_Xam_App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P_M_Xam_App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationSummaryPage : ContentPage
    {
        private int _currencyId;
        private int _areaId;
        private int _productId;
        private double _price;
        private string _desc;
        private DateTime _date;
        private string _currencyType;

        public ReservationSummaryPage(string productName, DateTime date, double price, string currencyType, int areaId, int productId)
        {
            InitializeComponent();
            LblProduct.Text = productName;
            LblStartDate.Text = date.ToString();
            LblCurrencyType.Text = currencyType;
            LblPrice.Text = price.ToString();
            _date = date;
            _price = price;
            _currencyType = currencyType;
            _productId = productId;
            _areaId = areaId;
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
            var response = await ApiService.PlaceOrderSummary(order);
            
            Application.Current.MainPage = new NavigationPage(new ReservationInfoPage(_date, _price, _currencyType, _areaId, _productId));
        }

    }
}