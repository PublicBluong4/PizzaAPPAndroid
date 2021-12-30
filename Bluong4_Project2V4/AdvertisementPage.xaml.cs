using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bluong4_Project2V4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvertisementPage : ContentPage
    {
        bool runTimer;
        public AdvertisementPage()
        {
            InitializeComponent();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnStartTimer_Clicked(object sender, EventArgs e)
        {
            runTimer = true;
            Device.StartTimer(new TimeSpan(0, 0, 2), () =>
              {
                  ChangeImageSource();
                  return runTimer;
              });
        }

        private void btnStopTimer_Clicked(object sender, EventArgs e)
        {
            runTimer = false;
        }

        private void ChangeImageSource()
        {
            List<string> listImageSource = new List<string>() { "Pizza1.png", "Pizza2.png", "Pizza3.png", "Pizza4.png", "Pizza5.png", "Pizza6.png" };
            var rand = new Random();
            int nextIndex = rand.Next(0, listImageSource.Count);
            imageSlide.Source = listImageSource[nextIndex];

        }
    }
}