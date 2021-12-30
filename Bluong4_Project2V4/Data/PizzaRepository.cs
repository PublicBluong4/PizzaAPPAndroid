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
    class PizzaRepository : IPizzaRepository
    {
        HttpClient client = new HttpClient();
        public PizzaRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Pizza>> GetPizzas()
        {
            var response = await client.GetAsync("api/pizzas");
            if(response.IsSuccessStatusCode)
            {
                List<Pizza> pizzas = await response.Content.ReadAsAsync<List<Pizza>>();
                return pizzas;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task<List<Pizza>> GetPizzaByPizzaType(int ID)
        {
            var response = await client.GetAsync($"api/pizzas/byPizzaType/{ID}");
            if(response.IsSuccessStatusCode)
            {
                List<Pizza> pizzas = await response.Content.ReadAsAsync<List<Pizza>>();
                return pizzas;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task<Pizza> GetPizza(int ID)
        {
            var response = await client.GetAsync($"api/pizzas/{ID}");
            if(response.IsSuccessStatusCode)
            {
                Pizza pizza = await response.Content.ReadAsAsync<Pizza>();
                return pizza;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task AddPizza(Pizza PizzaToAdd)
        {
            var response = await client.PostAsJsonAsync("api/pizzas", PizzaToAdd);
            if(!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task UpdatePizza(Pizza PizzaToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/pizzas/{PizzaToUpdate.ID}", PizzaToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
        public async Task DeletePizza(Pizza PizzaToDelete)
        {
            var response = await client.DeleteAsync($"api/pizzas/{PizzaToDelete.ID}");
            if(!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
