using System;
using System.Reflection;


public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}


public class Product
{
    public string ProductName { get; set; }
    public double Price { get; set; }
}

class Program
{
    
    public static void SerializeObject(object obj)
    {
        Type type = obj.GetType();
        Console.WriteLine($"\nProperties of {type.Name}:");

        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(obj);
            Console.WriteLine($"{property.Name} = {value}");
        }
    }

   
    static string ReadString(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    
    static int ReadInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
                break;
            Console.WriteLine("Invalid input! Please enter a valid integer.");
        }
        return value;
    }

   
    static double ReadDouble(string prompt)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out value))
                break;
            Console.WriteLine("Invalid input! Please enter a valid number.");
        }
        return value;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Choose object type to create: 1. Person 2. Product");
        string choice = ReadString("Enter choice (1 or 2): ");

        if (choice == "1")
        {
            string name = ReadString("Enter Name: ");
            int age = ReadInt("Enter Age: ");

            Person person = new Person { Name = name, Age = age };
            SerializeObject(person);
        }
        else if (choice == "2")
        {
            string productName = ReadString("Enter Product Name: ");
            double price = ReadDouble("Enter Price: ");

            Product product = new Product { ProductName = productName, Price = price };
            SerializeObject(product);
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
    }
}
