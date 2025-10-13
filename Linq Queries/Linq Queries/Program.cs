using System;
using System.Collections.Generic;
using System.Linq;
class Product
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public double Price { get; set; }
}
class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>
        {
            new Product { Name = "Laptop", Category = "Electronics", Price = 75000 },
            new Product { Name = "Smartphone", Category = "Electronics", Price = 30000 },
            new Product { Name = "Desk", Category = "Furniture", Price = 15000 },
            new Product { Name = "shirts", Category = "Clothing", Price = 2000 }
        };
        Console.Write("Enter category name: ");
        string categoryInput = Console.ReadLine()??string.Empty;
        var selectedProducts = from p in products
                               where p.Category.Equals(categoryInput, StringComparison.OrdinalIgnoreCase)
                               select p;
        if (selectedProducts.Any())
        {
            Console.WriteLine($"\nProducts in category '{categoryInput}' :");
            foreach (var product in selectedProducts)
            {
                Console.WriteLine($"Name:{product.Name}, Price {product.Price}");
            }
            double avgPrice = selectedProducts.Average(p => p.Price);
            Console.WriteLine($"\nAverage price of products in '{categoryInput}':{avgPrice:F2}");
        }
        else
        {
            Console.WriteLine($"\nNo products found in category '{categoryInput}'.");
        }
    }
}
