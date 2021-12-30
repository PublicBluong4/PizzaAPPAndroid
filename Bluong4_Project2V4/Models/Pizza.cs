using System;
using System.Collections.Generic;
using System.Text;

namespace Bluong4_Project2V4.Models
{
    public class Pizza
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int PizzaTypeID { get; set; }
        public PizzaType PizzaType { get; set; }
    }
}
