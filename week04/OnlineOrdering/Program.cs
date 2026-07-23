using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("12 Aso Drive", "Abuja", "FCT", "Nigeria");
        Customer customer1 = new Customer("Olise Ebinum", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Mouse", "P1001", 15.99, 2));
        order1.AddProduct(new Product("USB-C Cable", "P1002", 8.50, 3));
        order1.AddProduct(new Product("Laptop Stand", "P1003", 29.99, 1));

        Address address2 = new Address("500 Main Street", "Rexburg", "Idaho", "USA");
        Customer customer2 = new Customer("Sarah Johnson", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Notebook", "P2001", 4.25, 5));
        order2.AddProduct(new Product("Pen Set", "P2002", 6.75, 2));

        Order[] orders = { order1, order2 };

        foreach (Order order in orders)
        {
            Console.WriteLine("Packing Label:");
            Console.WriteLine(order.GetPackingLabel());

            Console.WriteLine("Shipping Label:");
            Console.WriteLine(order.GetShippingLabel());

            Console.WriteLine($"Total Price: ${order.GetTotalPrice():F2}");
            Console.WriteLine();
        }
    }
}