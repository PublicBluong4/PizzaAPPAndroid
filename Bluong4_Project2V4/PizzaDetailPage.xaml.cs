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
    public partial class PizzaDetailPage : ContentPage
    {
        App thisApp;
        Pizza pizza;
        public PizzaDetailPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pizza = (Pizza)this.BindingContext;
            setPizzaType();
            if(pizza.ID == 0) //Adding new
            {
                this.Title = "Add new Pizza";
                btnDelete.IsEnabled = false;

            }
            else
            {
                this.Title = "Edit Pizza detail";
                btnDelete.IsEnabled = true;
            }
        }

        void setPizzaType()
        {
            ddlPizzaTypes.ItemsSource = thisApp.AllPizzaTypes;
            if(pizza.PizzaType != null)
            {
                int selectedIndex = -1;
                for(int i = 0; i <= thisApp.AllPizzaTypes.Count-1; i++)
                {
                    if (thisApp.AllPizzaTypes[i].ID == pizza.PizzaTypeID)
                        selectedIndex = i;
                }
                ddlPizzaTypes.SelectedIndex = selectedIndex;
            }
            else
            {
                ddlPizzaTypes.SelectedItem = new PizzaType { ID = 0, Type = "Select a Pizza Type" };
            }
        }

        int getPizzaTypeID()
        {
            if(ddlPizzaTypes.SelectedIndex == -1)
            {
                return -1;
            }
            if(((PizzaType)ddlPizzaTypes.SelectedItem).Type != "Select a Pizza Type")
            {
                return ((PizzaType)ddlPizzaTypes.SelectedItem).ID;
            }
            else
            {
                return 0;
            }
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            try
            {
                pizza.PizzaTypeID = getPizzaTypeID();
                if(pizza.PizzaTypeID > 0)
                {
                    PizzaRepository r = new PizzaRepository();
                    if(pizza.ID == 0)
                    {
                        await r.AddPizza(pizza);
                    }
                    else
                    {
                        await r.UpdatePizza(pizza);
                    }
                    thisApp.needPizzaTypeRefresh = true;
                    await Navigation.PopAsync();

                }
                else
                {
                    await DisplayAlert("Pizza Type Not Selected:", "You must set the Pizza Type for the Pizza.", "Ok");
                }
            }
            catch (AggregateException ex)
            {
                string errMsg = "";
                foreach (var exception in ex.InnerExceptions)
                {
                    errMsg += Environment.NewLine + exception.Message;
                }
                await DisplayAlert("One or more exceptions has occurred:", errMsg, "Ok");
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                thisApp.needPizzaTypeRefresh = true;
                await DisplayAlert("Problem Saving the Pizza:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    await DisplayAlert("Error", "No connection with the server.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "Could not complete operation.", "Ok");
                }
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete" + pizza.Name + "?", "Yes", "No");
            if(answer == true)
            {
                try
                {
                    PizzaRepository dl = new PizzaRepository();
                    await dl.DeletePizza(pizza);
                    thisApp.needPizzaTypeRefresh = true;
                    await Navigation.PopAsync();
                }
                catch (AggregateException ex)
                {
                    string errMsg = "";
                    foreach (var exception in ex.InnerExceptions)
                    {
                        errMsg += Environment.NewLine + exception.Message;
                    }
                    await DisplayAlert("One or more exceptions has occurred:", errMsg, "Ok");
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    await DisplayAlert("Problem Deleting the Pizza:", sb.ToString(), "Ok");
                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {
                        await DisplayAlert("Error", "No connection with the server.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Could not complete operation.", "Ok");
                    }
                }
            }
        }
    }
}