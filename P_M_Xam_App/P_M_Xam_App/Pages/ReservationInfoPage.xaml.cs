using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_M_Xam_App.ApiServices;
using P_M_Xam_App.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P_M_Xam_App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationInfoPage : ContentPage
    {
        
        private int _areaId;
        private int _productId;
        private double _price;
        private DateTime _date;
        private string _currencyType;
        

        public ReservationInfoPage(DateTime date, double price, string currencyType, int areaId, int productId)
        {
            InitializeComponent();
            LblCurrency.Text = currencyType;
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
                EntFirstName.Text,
                EntLastName.Text,
                EntVehicleReg.Text
            };
            var response = await ApiService.NewCard(order);
            var orderId = response.OrderId;
            Application.Current.MainPage = new NavigationPage(new CompleteOrderDetailPage(_date, _price, _currencyType, _areaId, _productId, orderId));
        }
    }
}