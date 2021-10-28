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
    public partial class AreasPage : ContentPage
    {
        public ObservableCollection<AreaDetails> AreaDetailsCollection;
        private string _currenArea;


        public AreasPage(int areaId, string areaName)
        {
            InitializeComponent();
            LblAreaName.Text = areaName;
            AreaDetailsCollection = new ObservableCollection<AreaDetails>();
            _currenArea = areaName;
            GetAreas(areaId);

        }


        private async void GetAreas(int areaId)
        {
            var products = await ApiService.GetArea(areaId);
            foreach (var product in products)
            {
                AreaDetailsCollection.Add(product);
            }
            CvAreas.ItemsSource = AreaDetailsCollection;
        }

        private void CvAreas_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as AreaDetails;
            if (currentSelection == null)
            {
                return;
            }

            _currenArea = currentSelection.AreaName;
            Navigation.PushModalAsync(new PlaceOrderPage(currentSelection.AreaID, currentSelection.AreaName, currentSelection.CurID));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}