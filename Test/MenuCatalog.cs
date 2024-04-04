using System;
using System.Collections.Generic;
using Test;

public class MenuCatalog
{
    private Dictionary<int, Pizza> pizzas = new Dictionary<int, Pizza>();

    public void AddPizza(Pizza pizza)
    {
        pizzas[pizza.Number] = pizza;
    }

    public bool RemovePizza(int number)
    {
        return pizzas.Remove(number);
    }

    public void UpdatePizza(Pizza pizza)
    {
        pizzas[pizza.Number] = pizza;
    }

    public Pizza SearchPizza(int number)
    {
        pizzas.TryGetValue(number, out Pizza pizza);
        return pizza;
    }

    public void PrintMenu()
    {
        foreach (var pizza in pizzas.Values)
        {
            Console.WriteLine($"Number: {pizza.Number}, Name: {pizza.Name}, Price: {pizza.Price}");
        }
    }

    public bool DeletePizza(int pizzaId)
    {
        return pizzas.Remove(pizzaId);
    }

    public void UpdatePizza(int pizzaId, string newName, decimal newPrice)
    {
        if (pizzas.ContainsKey(pizzaId))
        {
            Pizza pizza = pizzas[pizzaId];
            pizza.Name = newName;
            pizza.Price = newPrice;
        }
    }
}

