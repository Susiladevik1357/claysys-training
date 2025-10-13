using System;
using System.Collections.Generic;

namespace ECommerceApp
{
    // Product class
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Quantity;

        // Constructor
        public Product(int id, string name, double price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        // Method to display product details
        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - Rs {Price} - Stock: {Quantity}");
        }
    }

    // Shopping cart class
    class ShoppingCart
    {
        public List<Product> cartItems = new List<Product>();

        // Add product to cart
        public void AddProduct(Product p, int qty)
        {
            Product item = new Product(p.Id, p.Name, p.Price, qty);
            cartItems.Add(item);
        }

        // Remove product from cart
        public void RemoveProduct(int id)
        {
            bool found = false;
            for (int i = 0; i < cartItems.Count; i++)
            {
                if (cartItems[i].Id == id)
                {
                    cartItems.RemoveAt(i);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Product not found in cart.");
            }
        }

        // Display cart items
        public void DisplayCart()
        {
            if (cartItems.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
            }
            else
            {
                Console.WriteLine("\nItems in your cart:");
                foreach (Product item in cartItems)
                {
                    Console.WriteLine($"{item.Name} - Qty: {item.Quantity} - Rs {item.Price * item.Quantity}");
                }
            }
        }

        // Calculate total amount
        public double CalculateTotal()
        {
            double total = 0;
            foreach (Product item in cartItems)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create some sample products
            List<Product> products = new List<Product>();
            products.Add(new Product(1, "T-Shirt", 500, 10));
            products.Add(new Product(2, "Towel", 100, 5));
            products.Add(new Product(3, "Shoes", 2500, 3));

            ShoppingCart cart = new ShoppingCart();

            while (true)
            {
                Console.WriteLine("\n=== E-Commerce Menu ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Remove from Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                int choice;

                // Error handling for invalid input
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                if (choice == 1)
                {
                    Console.WriteLine("\nAvailable Products:");
                    foreach (Product p in products)
                    {
                        p.DisplayProduct();
                    }
                }
                else if (choice == 2)
                {
                    Console.Write("Enter Product ID to add: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Product selected = null;

                    foreach (Product p in products)
                    {
                        if (p.Id == id)
                        {
                            selected = p;
                            break;
                        }
                    }

                    if (selected == null)
                    {
                        Console.WriteLine("Invalid Product ID.");
                    }
                    else
                    {
                        Console.Write("Enter quantity: ");
                        int qty = Convert.ToInt32(Console.ReadLine());

                        if (qty > 0 && qty <= selected.Quantity)
                        {
                            cart.AddProduct(selected, qty);
                            selected.Quantity -= qty;
                            Console.WriteLine($"{qty} {selected.Name}(s) added to cart.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity or not enough stock.");
                        }
                    }
                }
                else if (choice == 3)
                {
                    cart.DisplayCart();
                }
                else if (choice == 4)
                {
                    Console.Write("Enter Product ID to remove: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    cart.RemoveProduct(id);
                }
                else if (choice == 5)
                {
                    Console.WriteLine("\n--- Checkout ---");
                    cart.DisplayCart();
                    double total = cart.CalculateTotal();
                    Console.WriteLine($"Total Amount: Rs {total}");
                    Console.WriteLine("Thank you for shopping!");
                    break;
                }
                else if (choice == 6)
                {
                    Console.WriteLine("Exiting... Thank you!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
        }
    }
}
