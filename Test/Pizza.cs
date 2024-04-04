using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Pizza
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Pizza(int number, string name, decimal price)
        {
            Number = number;
            Name = name;
            Price = price;
        }
    }

}
