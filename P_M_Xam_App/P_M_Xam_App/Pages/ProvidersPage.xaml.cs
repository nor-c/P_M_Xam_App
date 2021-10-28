using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ProvidersPage : ContentPage
    {
        public ObservableCollection<Provider> ProvidersCollection;
        public ProvidersPage()
        {
            InitializeComponent();
            ProvidersCollection = new ObservableCollection<Provider>();
            GetProviders();
        }

        private async void GetProviders()
        {
            var products = await ApiService.GetProviders();
            foreach (var product in products)
            {
                ProvidersCollection.Add(product);
            }
            CvProviders.ItemsSource = ProvidersCollection;
        }

        private void TapLogout_OnTapped(object sender, EventArgs e)
        {
            Preferences.Set("accessToken", string.Empty);
            Preferences.Set("tokenExpirationTime", 0);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void CvProviders_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Provider;
            if (currentSelection == null)
            {
                return;
            }

            Navigation.PushModalAsync(new AreasPage(currentSelection.ProviderID, currentSelection.ProviderName));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}