using Bluong4_Project2V4.Data;
using Bluong4_Project2V4.Models;
using Bluong4_Project2V4.Utilities;
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
    public partial class Menu : ContentPage
    {
        App thisApp;
        List<PizzaType> pizzaTypes;
        List<Pizza> pizzas;
        public Menu()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            pizzaTypes = new List<PizzaType>();
            if(thisApp.AllPizzaTypes != null)
            {
                thisApp.AllPizzaTypes.Clear();
            }
            
            thisApp.needPizzaTypeRefresh = true;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await showPizzaTypes();
            if(thisApp.needPizzaTypeRefresh)
            {
                refreshPizza();
            }
            else
            {
                lstViewPizza.IsVisible = true;
            }
            lstViewPizza.SelectedItem = null;
        }
        private async Task showPizzaTypes()
        {
            if(!(thisApp.AllPizzaTypes?.Count > 0))
            {
                //Get the PizzaType
                PizzaTypeRepository r = new PizzaTypeRepository();
                try
                {
                    pizzaTypes.Add(new PizzaType { ID = 00, Type = "All Pizza Type" });
                    thisApp.AllPizzaTypes = await r.GetPizzaTypes();
                    foreach(PizzaType p in thisApp.AllPizzaTypes.OrderBy(p=>p.Type))
                    {
                        pizzaTypes.Add(p);
                    }
                    ddlPizzaTypes.ItemDisplayBinding = new Binding("Type");
                    ddlPizzaTypes.ItemsSource = pizzaTypes;
                    ddlPizzaTypes.SelectedIndex = 0;

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.GetBaseException().Message.Contains("connection with the server"))
                        {

                            await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                        }
                    }
                    else
                    {
                        if (ex.Message.Contains("NameResolutionFailure"))
                        {
                            await DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Jeeves.DBUri.ToString(), "Ok");
                        }
                        else
                        {
                            await DisplayAlert("General Error ", ex.Message, "Ok");
                        }

                    }
                }
            }
            
        }
        private async Task showPizza(int? PizzaTypeID)
        {
            //Get the Pizzas
            PizzaRepository r = new PizzaRepository();
            try
            {
                List<Pizza> pizzas;
                if(PizzaTypeID.GetValueOrDefault() > 0)
                {
                    pizzas = await r.GetPizzaByPizzaType(PizzaTypeID.GetValueOrDefault());
                }
                else
                {
                    pizzas = await r.GetPizzas();
                }
                lstViewPizza.ItemsSource = pizzas;
                lstViewPizza.IsVisible = true;
                lstPizzaFrame.IsVisible = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {

                        await DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("General Error", "If the problem persists, please call your system administrator.", "Ok");
                }

            }
        }
        private async void refreshPizza()
        {
            thisApp = Application.Current as App;
            if(ddlPizzaTypes.SelectedIndex < 1)
            {
                await showPizza(null);
            }
            else
            {
                int? id = ((PizzaType)ddlPizzaTypes.SelectedItem).ID;
                await showPizza(id.GetValueOrDefault());
            }
            thisApp.needPizzaTypeRefresh = false;
        }

        private void ddlPizzaTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshPizza();
        }

        private void lstViewPizza_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                var pizza = (Pizza)e.SelectedItem;
                var pizzaDetailPage = new PizzaDetailPage();
                pizzaDetailPage.BindingContext = pizza;
                Navigation.PushAsync(pizzaDetailPage);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void AddPizza_Clicked(object sender, EventArgs e)
        {
            Pizza newPizza = new Pizza();
            var pizzaDetailPage = new PizzaDetailPage();
            pizzaDetailPage.BindingContext = newPizza;
            lstPizzaFrame.IsVisible = false;
            Navigation.PushAsync(pizzaDetailPage);
        }
    }
}