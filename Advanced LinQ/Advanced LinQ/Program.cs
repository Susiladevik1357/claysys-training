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
        var groupByCategory = from p in products
                              group p by p.Category into g
                              select new
                              {
                                  Category = g.Key,
                                  Count = g.Count(),
                                  AveragePrice = g.Average(x => x.Price)
                              };
        var orderedGroups = groupByCategory.OrderByDescending(g => g.Count);
        Console.WriteLine("Category summary(grouped and ordered by count):\n");
        foreach (var group in orderedGroups)
        {
            Console.WriteLine($"Category:{group.Category}, Count:{group.Count}, Average Price:{group.AveragePrice:F2}");
        }
    }
}