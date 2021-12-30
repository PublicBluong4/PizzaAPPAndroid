using Bluong4_Project2V4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bluong4_Project2V4.Data
{
    interface IPizzaRepository
    {
        Task<List<Pizza>> GetPizzas();
        Task<List<Pizza>> GetPizzaByPizzaType(int ID);
        Task<Pizza> GetPizza(int ID);
        Task AddPizza(Pizza PizzaToAdd);
        Task UpdatePizza(Pizza PizzaToUpdate);
        Task DeletePizza(Pizza PizzaToDelete);

    }
}
