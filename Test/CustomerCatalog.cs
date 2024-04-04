using System.Collections.Generic;
using System.Linq;
using Test;

public class CustomerCatalog
{
    private Dictionary<int, Customer> customers = new Dictionary<int, Customer>();

    public void AddCustomer(Customer customer)
    {
        customers[customer.Id] = customer;
    }

    public bool RemoveCustomer(int id)
    {
        return customers.Remove(id);
    }

    public void UpdateCustomer(Customer customer)
    {
        if (customers.ContainsKey(customer.Id))
        {
            customers[customer.Id] = customer;
        }
    }
    public List<Customer> SearchCustomersByName(string name)
    {
        return customers.Values.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return customers.Values;
    }

    public bool DeleteCustomer(int customerId)
    {
        return customers.Remove(customerId);
    }

    public void UpdateCustomer(int customerId, string newName)
    {
        if (customers.ContainsKey(customerId))
        {
            Customer customer = customers[customerId];
            customer.Name = newName;
        }
    }
}

