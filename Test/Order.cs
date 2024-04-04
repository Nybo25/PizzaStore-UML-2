using System.Collections.Generic;
using Test;

public class Order
{
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    public List<Pizza> Pizzas { get; private set; }
    public Dictionary<Pizza, List<(string topping, decimal price)>> ExtraToppings { get; private set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }

    public Order(int orderId, Customer customer)
    {
        OrderId = orderId;
        Customer = customer;
        Pizzas = new List<Pizza>();
        ExtraToppings = new Dictionary<Pizza, List<(string, decimal)>>();
    }

    public void AddPizza(Pizza pizza)
    {
        Pizzas.Add(pizza);
        if (!ExtraToppings.ContainsKey(pizza))
        {
            ExtraToppings[pizza] = new List<(string, decimal)>();
        }
    }

    public void AddExtraToppingToPizza(Pizza pizza, string topping, decimal price)
    {
        if (ExtraToppings.ContainsKey(pizza))
        {
            ExtraToppings[pizza].Add((topping, price));
        }
    }

    public decimal TotalPrice
    {
        get
        {
            decimal pizzaPrice = Pizzas.Sum(p => p.Price);
            decimal toppingsPrice = ExtraToppings.Sum(et => et.Value.Sum(t => t.price));
            return pizzaPrice + toppingsPrice;
        }
    }
}



