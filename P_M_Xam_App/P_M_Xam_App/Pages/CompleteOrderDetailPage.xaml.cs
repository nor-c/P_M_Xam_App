using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CompleteOrderDetailPage : ContentPage
    {
        private int _areaId;
        private int _productId;
        private double _price;
        private DateTime _date;
        private string _currencyType;
        private int _orderId;

        public CompleteOrderDetailPage(DateTime date, double price, string currencyType, int areaId, int productId, int orderId)
        {
            InitializeComponent();
            LblAreaName.Text = areaId.ToString();
            LblProductId.Text = productId.ToString();
            LblStartDate.Text = date.ToString();
            LblCurrency.Text = currencyType;
            LblPrice.Text = price.ToString();
            _date = date;
            _price = price;
            _currencyType = currencyType;
            _productId = productId;
            _areaId = areaId;
            _orderId = orderId;
        }
    }
}