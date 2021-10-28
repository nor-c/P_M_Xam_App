using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_M_Xam_App.ApiServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P_M_Xam_App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void TapLogin_OnTapped(object sender, EventArgs e)
        {
            var response = await ApiService.Login(EntAuthId.Text, EntPassword.Text);
            Preferences.Set("authID", EntAuthId.Text);
            Preferences.Set("credentials", EntPassword.Text);
            if (response)
            {
                Application.Current.MainPage = new NavigationPage(new ProvidersPage());
            }
            else
            {
                await DisplayAlert("Oops", "Email or Password is incorrect or not exists", "Ok");

            }
        }

        private void SpanSignup_OnTapped(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}