using System;
using System.Collections.Generic;
using System.Linq;
using Test;

public class Store
{
    private MenuCatalog menuCatalog = new MenuCatalog();
    private CustomerCatalog customerCatalog = new CustomerCatalog();
    private List<Order> orders = new List<Order>();
    private UserDialog userDialog = new UserDialog();

    public Store()
    {
        InitializeMenu(); 
    }

    private void InitializeMenu()
    {
        menuCatalog.AddPizza(new Pizza(1, "Margherita", 55m));
        menuCatalog.AddPizza(new Pizza(2, "Pepperoni", 60m));
        menuCatalog.AddPizza(new Pizza(3, "Hawaiian", 65m));
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("1. Add Pizza to Menu");
            Console.WriteLine("2. Display Menu");
            Console.WriteLine("3. Register Customer");
            Console.WriteLine("4. Create Order");
            Console.WriteLine("5. Display Orders");
            Console.WriteLine("6. Delete Pizza from Menu");
            Console.WriteLine("7. Update Pizza in Menu");
            Console.WriteLine("8. Delete Customer");
            Console.WriteLine("9. Update Customer");
            Console.WriteLine("10. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddPizza();
                    break;
                case "2":
                    DisplayMenu();
                    break;
                case "3":
                    RegisterCustomer();
                    break;
                case "4":
                    CreateOrder();
                    break;
                case "5":
                    DisplayOrders();
                    break;
                case "6":
                    DeletePizza();
                    break;
                case "7":
                    UpdatePizza();
                    break;
                case "8":
                    DeleteCustomer();
                    break;
                case "9":
                    UpdateCustomer();
                    break;
                case "10":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private void AddPizza()
    {
        Console.Write("Enter pizza number: ");
        int number = int.Parse(Console.ReadLine());
        Console.Write("Enter pizza name: ");
        string name = Console.ReadLine();
        Console.Write("Enter pizza price: ");
        decimal price = decimal.Parse(Console.ReadLine());

        menuCatalog.AddPizza(new Pizza(number, name, price));
        Console.WriteLine($"{name} added to the menu.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void DisplayMenu()
    {
        menuCatalog.PrintMenu();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void RegisterCustomer()
    {
        Console.Write("Enter customer ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter customer name: ");
        string name = Console.ReadLine();

        customerCatalog.AddCustomer(new Customer(id, name));
        Console.WriteLine($"{name} registered successfully.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void CreateOrder()
    {
        Console.Write("Enter order ID: ");
        int orderId = int.Parse(Console.ReadLine());
        Console.Write("Enter customer ID: ");
        int customerId = int.Parse(Console.ReadLine());

        Customer customer = customerCatalog.GetAllCustomers().FirstOrDefault(c => c.Id == customerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        Order order = new Order(orderId, customer);

        bool addingPizzas = true;
        while (addingPizzas)
        {
            Console.Write("Enter pizza number to add to the order (or 'done' to finish adding pizzas): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "done")
            {
                addingPizzas = false;
                continue;
            }

            int pizzaNumber = int.Parse(input);
            Pizza pizza = menuCatalog.SearchPizza(pizzaNumber);
            if (pizza != null)
            {
                order.AddPizza(pizza);
                Console.WriteLine($"{pizza.Name} added. Add toppings to this pizza? (yes/no): ");
                string addToppingsAnswer = Console.ReadLine();
                if (addToppingsAnswer.ToLower() == "yes")
                {
                    AddToppingsToPizza(order, pizza);
                }
            }
            else
            {
                Console.WriteLine("Pizza not found.");
            }
        }

        orders.Add(order);
        Console.WriteLine($"Order {orderId} created for {customer.Name}. Total Price: {order.TotalPrice:C}");
    }

    private void AddToppingsToPizza(Order order, Pizza pizza)
    {
        bool addingToppings = true;
        while (addingToppings)
        {
            Console.Write("Enter topping name (or 'done' to finish adding toppings): ");
            string toppingName = Console.ReadLine();
            if (toppingName.ToLower() == "done")
            {
                addingToppings = false;
                continue;
            }

            Console.Write("Enter topping price: ");
            decimal toppingPrice = decimal.Parse(Console.ReadLine());

            order.AddExtraToppingToPizza(pizza, toppingName, toppingPrice);
            Console.WriteLine($"{toppingName} added to {pizza.Name}.");
        }
    }


    private void DisplayOrders()
    {
        if (orders.Count == 0)
        {
            Console.WriteLine("There are no orders to display.");
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Customer: {order.Customer.Name}");

            if (order.Pizzas.Count > 0)
            {
                Console.WriteLine("Pizzas in this order:");
                foreach (var pizza in order.Pizzas)
                {
                    Console.WriteLine($"- {pizza.Name}, Price: {pizza.Price:C}");
                }

                if (order.ExtraToppings.Count > 0)
                {
                    Console.WriteLine("Extra toppings:");
                    foreach (var topping in order.ExtraToppings)
                    {
                        foreach (var extra in topping.Value)
                        {
                            Console.WriteLine($"-- {extra.topping}, Price: {extra.price:C}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No pizzas in this order.");
            }

            Console.WriteLine($"Total Price: {order.TotalPrice:C}");
            Console.WriteLine("-------------");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void DeleteCustomer()
    {
        Console.Write("Enter the ID of the customer to delete: ");
        int id = int.Parse(Console.ReadLine());
        if (customerCatalog.DeleteCustomer(id))
        {
            Console.WriteLine("Customer deleted successfully.");
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }

    private void UpdateCustomer()
    {
        Console.Write("Enter the ID of the customer to update: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter the new name for the customer: ");
        string newName = Console.ReadLine();
        customerCatalog.UpdateCustomer(id, newName);
        Console.WriteLine("Customer updated successfully.");
    }

    private void DeletePizza()
    {
        Console.Write("Enter the number of the pizza to delete: ");
        int number = int.Parse(Console.ReadLine());
        if (menuCatalog.DeletePizza(number))
        {
            Console.WriteLine("Pizza deleted successfully.");
        }
        else
        {
            Console.WriteLine("Pizza not found.");
        }
    }

    private void UpdatePizza()
    {
        Console.Write("Enter the number of the pizza to update: ");
        int number = int.Parse(Console.ReadLine());
        Console.Write("Enter the new name for the pizza: ");
        string newName = Console.ReadLine();
        Console.Write("Enter the new price for the pizza: ");
        decimal newPrice = decimal.Parse(Console.ReadLine());
        menuCatalog.UpdatePizza(number, newName, newPrice);
        Console.WriteLine("Pizza updated successfully.");
    }

}


