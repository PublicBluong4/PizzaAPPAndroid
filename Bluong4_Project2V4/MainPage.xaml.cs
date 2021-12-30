using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bluong4_Project2V4
{
    public partial class MainPage : ContentPage
    {
        App thisApp;

        public MainPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;

        }

        private void btnMenu_Clicked(object sender, EventArgs e)
        {
            var menuPage = new Menu();
            Navigation.PushAsync(menuPage);
        }

        private void btnOrder_Clicked(object sender, EventArgs e)
        {
            var orderPage = new OrderPage();
            Navigation.PushAsync(orderPage);
        }

        private void btnReservations_Clicked(object sender, EventArgs e)
        {
            var reservationsPage = new ReservationsPage();
            Navigation.PushAsync(reservationsPage);
        }

        private void btnWallpaper_Clicked(object sender, EventArgs e)
        {
            var wallpaper = new SetWallpaper();
            Navigation.PushAsync(wallpaper);
        }

        private void btnMobile_Clicked(object sender, EventArgs e)
        {
            var getEquipmentInfo = new EquipmentInfo();
            Navigation.PushAsync(getEquipmentInfo);
        }

        private void btnAdvertise_Clicked(object sender, EventArgs e)
        {
            var advertisementPage = new AdvertisementPage();
            Navigation.PushAsync(advertisementPage);
        }
    }
}
