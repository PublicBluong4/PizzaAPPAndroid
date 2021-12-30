using Bluong4_Project2V4.Data;
using Bluong4_Project2V4.Models;
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
    public partial class SetWallpaper : ContentPage
    {
        public SetWallpaper()
        {
            InitializeComponent();
        }
        private async void btnSetWallpaper_Clicked(object sender, EventArgs e)
        {
            try
            {

                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Alert", "Accept for application access storage to use this function.", "OK");
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
                if (status == PermissionStatus.Granted)
                {
                    WallpaperDatabase database = await WallpaperDatabase.Instance;
                    List<Wallpaper> listWallpaper = await database.GetItemsAsync();
                    if (listWallpaper.Count > 0)
                    {
                        lblMessage.Text = "Wallpaper has already setted, don't need to set again.";
                        return;
                    }
                    else
                    {
                        ISetWallPaper setWallpaper = DependencyService.Get<ISetWallPaper>();
                        Wallpaper oldWallpaper = setWallpaper.SetWallPaper();
                        if (listWallpaper.Count == 0)
                        {
                            await database.SaveItemAsync(oldWallpaper);
                        }
                        lblMessage.Text = "Wallpaper setted successful.";
                    }
                }
                else
                {
                    lblMessage.Text = "Cannot set Wallpaper.";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRestore_Clicked(object sender, EventArgs e)
        {
            RestoreWallpaper();
            lblMessage.Text = "Restore wallpaper successful";
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


        public static async void RestoreWallpaper()
        {
            WallpaperDatabase databaseDelete = await WallpaperDatabase.Instance;
            List<Wallpaper> listWallpaperDelete = await databaseDelete.GetItemsAsync();
            Wallpaper wallpaperForRestore = new Wallpaper();
            if (listWallpaperDelete.Count > 0)
            {
                foreach (Wallpaper i in listWallpaperDelete)
                {
                    wallpaperForRestore = i;
                }
                ISetWallPaper getWallpaper = DependencyService.Get<ISetWallPaper>();
                object result = getWallpaper.RestoreWallPaper(wallpaperForRestore.Content);

                foreach (Wallpaper i in listWallpaperDelete)
                {
                    await databaseDelete.DeleteItemAsync(i);
                }
            }

        }
    }
}