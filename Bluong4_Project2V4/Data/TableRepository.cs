using Bluong4_Project2V4.Models;
using Bluong4_Project2V4.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bluong4_Project2V4.Data
{
    public class TableRepository : ITableRepository
    {
        HttpClient client = new HttpClient();
        public TableRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Table>> GetTables()
        {
            var response = await client.GetAsync("api/tables");
            if(response.IsSuccessStatusCode)
            {
                List<Table> listTables = await response.Content.ReadAsAsync<List<Table>>();
                return listTables;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task<List<Table>> GetTableByStatus(bool status)
        {
            var response = await client.GetAsync($"api/tables/byTableStatus/{status}");
            if (response.IsSuccessStatusCode)
            {
                List<Table> listTables = await response.Content.ReadAsAsync<List<Table>>();
                return listTables;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task<Table> GetTable(int ID)
        {
            var response = await client.GetAsync($"api/tables/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Table table = await response.Content.ReadAsAsync<Table>();
                return table;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task UpdateTable(Table tableToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/tables/{tableToUpdate.ID}", tableToUpdate);

            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
