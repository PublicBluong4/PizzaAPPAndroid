using Bluong4_Project2V4.Calc;
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
    public partial class OrderPage : ContentPage
    {
        App thisApp;
        List<PizzaType> pizzaTypes;
        List<Pizza> pizzas;
        private int amount;
        private decimal pricePizzaSelected;
        private decimal totalBeforeTax;
        private decimal totalAfterTax;

        public OrderPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            if(thisApp.AllPizzaTypesForOrder != null)
            {
                thisApp.AllPizzaTypesForOrder.Clear();
            }
            pizzaTypes = new List<PizzaType>();
            thisApp.needOrderRefresh = true;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await showPizzaTypes();
            if (thisApp.needOrderRefresh)
            {
                refreshPizza();
            }
            else
            {
                ddlPizzas.IsVisible = true;
            }
            ddlPizzas.SelectedItem = null;
        }
        private async Task showPizzaTypes()
        {
            if (!(thisApp.AllPizzaTypesForOrder?.Count > 0))
            {
                //Get the PizzaType
                PizzaTypeRepository r = new PizzaTypeRepository();
                try
                {
                    pizzaTypes.Add(new PizzaType { ID = 0, Type = "All Pizza Type" });
                    thisApp.AllPizzaTypesForOrder = await r.GetPizzaTypes();
                    foreach (PizzaType p in thisApp.AllPizzaTypesForOrder.OrderBy(p => p.Type))
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
                List<Pizza> pizzas = new List<Pizza>();
                List<Pizza> temp;
                if (PizzaTypeID.GetValueOrDefault() > 0)
                {
                    temp = await r.GetPizzaByPizzaType(PizzaTypeID.GetValueOrDefault());
                }
                else
                {
                    temp = await r.GetPizzas();
                }
                pizzas.Add(new Pizza { ID = 0, Name = "Select a Pizza", Price = 0 , PizzaTypeID = 0});
                foreach (Pizza p in temp)
                {
                    pizzas.Add(p);
                }

                ddlPizzas.ItemsSource = pizzas;
                ddlPizzas.ItemDisplayBinding = new Binding("Name");
                ddlPizzas.SelectedIndex = 0;
                ddlPizzas.IsVisible = true;

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
            if (ddlPizzaTypes.SelectedIndex < 1)
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

        private void ddlPizzas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPrice.Text = "";
            if(ddlPizzas.SelectedItem != null)
            {
                Pizza selectedPizza = (Pizza)ddlPizzas.SelectedItem;
                if(selectedPizza.Name == "Select a Pizza")
                {
                    lblPrice.Text = "0";
                }
                else
                {
                    lblPrice.Text = selectedPizza.Price.ToString("C2");
                }
                
            }
        }

        private void btnOrder_Clicked(object sender, EventArgs e)
        {
            lblmessage.Text = "";
            lblTotalBeforeTax.Text = "";
            lblTotalAfterTax.Text = "";
            try
            {
                if(isValid())
                {
                    decimal tax = 1.13m;
                    pricePizzaSelected = ((Pizza)ddlPizzas.SelectedItem).Price;
                    totalBeforeTax = CalcPrice.totalBeforTax(pricePizzaSelected, amount);
                    totalAfterTax = CalcPrice.totalAfterTax(pricePizzaSelected, amount, tax);
                    lblTotalBeforeTax.Text = totalBeforeTax.ToString("C2");
                    lblTotalAfterTax.Text = totalAfterTax.ToString("C2");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool isValid()
        {
            bool validInput = true;
            string errMessage = "Please fix the following errors: " + Environment.NewLine;
            //Check for Quota
            if (String.IsNullOrEmpty(txtAmount.Text))
            {
                validInput = false;
                errMessage += "- No amount Pizza entered" + Environment.NewLine;
            }
            else if (!Int32.TryParse(txtAmount.Text, out amount))
            {
                validInput = false;
                errMessage += "- Amount must in number." + Environment.NewLine;
            }
            else if (amount < 0)
            {
                validInput = false;
                errMessage += "- Amount must greater than 0." + Environment.NewLine;
            }

            return validInput;
        }

        private void btnclear_Clicked(object sender, EventArgs e)
        {
            ddlPizzaTypes.SelectedIndex = 0;
            ddlPizzas.SelectedIndex = 0;
            lblPrice.Text = "";
            lblTotalAfterTax.Text = "";
            lblTotalBeforeTax.Text = "";
            lblmessage.Text = "";
            txtAmount.Text = "";
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}