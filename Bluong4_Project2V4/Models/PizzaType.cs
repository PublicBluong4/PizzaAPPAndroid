using System;
using System.Collections.Generic;
using System.Text;

namespace Bluong4_Project2V4.Models
{
    public class PizzaType
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public ICollection<Pizza> Pizzas { get; set; }
    }
}
