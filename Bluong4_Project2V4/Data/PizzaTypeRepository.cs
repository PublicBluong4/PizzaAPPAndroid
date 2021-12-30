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
    class PizzaTypeRepository : IPizzaTypeRepository
    {
        HttpClient client = new HttpClient();

        public PizzaTypeRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<PizzaType>> GetPizzaTypes()
        {
            var response = await client.GetAsync("api/pizzatypes");
            if(response.IsSuccessStatusCode)
            {
                List<PizzaType> pizzaTypes = await response.Content.ReadAsAsync<List<PizzaType>>();
                return pizzaTypes;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<PizzaType> GetPizzaType(int ID)
        {
            var response = await client.GetAsync($"api/pizzatypes/{ID}");
            if(response.IsSuccessStatusCode)
            {
                PizzaType pizzaType = await response.Content.ReadAsAsync<PizzaType>();
                return pizzaType;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddPizzaType(PizzaType PizzaTypeToAdd)
        {
            var response = await client.PostAsJsonAsync("api/pizzatypes", PizzaTypeToAdd);
            if(!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdatePizzaType(PizzaType PizzaTypeToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/pizzatypes/{PizzaTypeToUpdate.ID}", PizzaTypeToUpdate);
            if(!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeletePizzaType(PizzaType PizzaTypeToDelete)
        {
            var response = await client.DeleteAsync($"api/pizzatypes/{PizzaTypeToDelete.ID}");
            if(!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        } 
    }
}
