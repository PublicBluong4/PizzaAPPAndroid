using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Bluong4_Project2V4.Models;
using Android.Graphics;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using System.IO;
using Bluong4_Project2V4.Data;
using Android.Telephony;
using Bluong4_Project2V4.Droid;

[assembly: Dependency(typeof(MainActivity))]
namespace Bluong4_Project2V4.Droid
{
    [Activity(Label = "Bluong4_Project2V4", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ISetWallPaper
    {
        public object storeImage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Obsolete]
        public Wallpaper SetWallPaper()
        {
            try
            {
                WallpaperManager manager = WallpaperManager.GetInstance(Forms.Context.ApplicationContext);
                storeImage = manager.Drawable;
                Bitmap bitmap = BitmapFactory.DecodeResource(Forms.Context.Resources, Resource.Drawable.pizzaImage);
                manager.SetBitmap(bitmap);
                manager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.Lock);
                Bitmap defaultWallpaper = ((BitmapDrawable)storeImage).Bitmap;
                var memoryStream = new MemoryStream();
                defaultWallpaper.Compress(Bitmap.CompressFormat.Jpeg, 100, memoryStream);
                Wallpaper oldWallpaper = new Wallpaper();
                oldWallpaper.Name = "Second Wallpaper";
                oldWallpaper.Content = memoryStream.ToArray();
                oldWallpaper.StartTime = DateTime.Now;
                return oldWallpaper;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Obsolete]
        public object RestoreWallPaper(byte[] input)
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(input, 0, input.Length);
            WallpaperManager manager = WallpaperManager.GetInstance(Forms.Context.ApplicationContext);
            manager.SetBitmap(bitmap);
            manager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.Lock);
            return "Restore wallpaper successful";
        }

        [Obsolete]
        public string GetIdentifier()
        {
            string IMEI;
            try
            {
                var telephonyManager = (TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);
                IMEI = telephonyManager.DeviceId; // Example: “356457091758419” = This is the correct IMEI
            }
            catch (Exception)
            {

                throw;
            }
            return IMEI;
        }
    }
}