using Bluong4_Project2V4.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bluong4_Project2V4
{
    public partial class App : Application
    {
        public List<PizzaType> AllPizzaTypes;
        public List<PizzaType> AllPizzaTypesForOrder;
        public bool needPizzaTypeRefresh;
        public bool needOrderRefresh;
        public bool needTableRefresh;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            var nav = new NavigationPage (new MainPage() );
            nav.BarBackgroundColor = Color.FromHex("#A1EB5E28");
            MainPage = nav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
