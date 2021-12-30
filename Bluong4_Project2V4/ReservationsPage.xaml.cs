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
    public partial class ReservationsPage : ContentPage
    {
        App thisApp;
        List<Table> tables;
        public ReservationsPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            tables = new List<Table>();
            thisApp.needTableRefresh = true;
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await showTables();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnBook_Clicked(object sender, EventArgs e)
        {
            thisApp = Application.Current as App;
            if (ddlTables.SelectedItem != null)
            {
                if (ddlTables.SelectedIndex != 0)
                {
                    try
                    {
                        Table selectedTable = (Table)ddlTables.SelectedItem;
                        int tableID = selectedTable.ID;
                        TableRepository tb = new TableRepository();
                        Table tableToShow = await tb.GetTable(tableID);
                        tableToShow.Available = false;
                        await tb.UpdateTable(tableToShow);
                        lblTableName.Text = tableToShow.TableName;
                        lblTableAvailable.Text = tableToShow.Available.ToString();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    
                }
                else
                {
                    lblTableName.Text = "Please select table";
                    lblTableAvailable.Text = "NA";
                }

            }
        }

        private async void btnclear_Clicked(object sender, EventArgs e)
        {
            thisApp = Application.Current as App;
            if (ddlTables.SelectedItem != null)
            {
                if (ddlTables.SelectedIndex != 0)
                {
                    try
                    {
                        Table selectedTable = (Table)ddlTables.SelectedItem;
                        int tableID = selectedTable.ID;
                        TableRepository tb = new TableRepository();
                        Table tableToShow = await tb.GetTable(tableID);
                        tableToShow.Available = true;
                        await tb.UpdateTable(tableToShow);
                        lblTableName.Text = tableToShow.TableName;
                        lblTableAvailable.Text = tableToShow.Available.ToString();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                else
                {
                    lblTableName.Text = "Please select table";
                    lblTableAvailable.Text = "NA";
                }

            }
        }

        private async Task showTables()
        {
            try
            {
                TableRepository r = new TableRepository();
                List<Table> tempTableList = await r.GetTables();
                tables.Add(new Table { ID = 0, TableName = "Select a Table" });
                foreach (Table p in tempTableList.OrderBy(c=>c.TableName))
                {
                    tables.Add(p);
                }
                ddlTables.ItemsSource = tables;
                ddlTables.ItemDisplayBinding = new Binding("TableName");
                ddlTables.SelectedIndex = 0;
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

        private async void refreshTable()
        {

            lblTableName.Text = "";
            lblTableAvailable.Text = "";
            thisApp = Application.Current as App;
            if (ddlTables.SelectedItem != null)
            {
                if(ddlTables.SelectedIndex !=0)
                {
                    Table selectedTable = (Table)ddlTables.SelectedItem;
                    int tableID = selectedTable.ID;
                    TableRepository tb = new TableRepository();
                    Table tableToShow = await tb.GetTable(tableID);
                    lblTableName.Text = tableToShow.TableName;
                    lblTableAvailable.Text = tableToShow.Available.ToString();
                }
                else
                {
                    lblTableName.Text = "Please select table";
                    lblTableAvailable.Text = "NA";
                }
                
            }

        }

        private void ddlTables_SelectedIndexChanged(object sender, EventArgs e)
        {

            refreshTable();
            
        }
    }
}