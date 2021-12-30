using Bluong4_Project2V4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bluong4_Project2V4.Data
{
    interface IPizzaTypeRepository
    {
        Task<List<PizzaType>> GetPizzaTypes();
        Task<PizzaType> GetPizzaType(int ID);
        Task AddPizzaType(PizzaType PizzaTypeToAdd);
        Task UpdatePizzaType(PizzaType PizzaTypeToUpdate);
        Task DeletePizzaType(PizzaType PizzaTypeToDelete);

    }
}
