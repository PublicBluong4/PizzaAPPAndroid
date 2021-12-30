using Bluong4_Project2V4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bluong4_Project2V4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentInfo : ContentPage
    {
        public EquipmentInfo()
        {
            InitializeComponent();
        }

        private async void btnIMEI_Clicked(object sender, EventArgs e)
        {
            string Imei = "";
            var status = await Permissions.CheckStatusAsync<Permissions.Phone>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Alert", "Accept for application access phone state to use this function.", "OK");
                status = await Permissions.RequestAsync<Permissions.Phone>();
            }
            if (status == PermissionStatus.Granted)
            {
                ISetWallPaper setWallpaper = DependencyService.Get<ISetWallPaper>();
                Imei = setWallpaper.GetIdentifier();
            }
            lblMessage.Text = Imei;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}