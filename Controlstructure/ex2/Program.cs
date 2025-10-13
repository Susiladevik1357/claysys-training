using System;

namespace myCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaring variables
            int num1 = 10;
            int num2 = 5;
            double price = 10.5;
            bool isAvailable = true;
            char grade = 'A';
            string name = "Susila";

            //performing arithmetic operations
            int add = num1 + num2;
            int sub = num1 - num2;
            int mul = num1 * num2;
            double div = (double)num1 / num2;
            int mod = num1 % num2;

            //print
            Console.WriteLine("Arithmetic Operations:");
            Console.WriteLine("Addition: " + add);
            Console.WriteLine("Subtraction: " + sub);
            Console.WriteLine("Multiplication: " + mul);
            Console.WriteLine("Division: " + div);
            Console.WriteLine("Modulus: " + mod);
            Console.WriteLine(); //for space

            //using comparison operators
            Console.WriteLine("Comparison operations:");
            Console.WriteLine("num1>num2: " + (num1 > num2));
            Console.WriteLine("num1=num2: " + (num1 == num2));
            Console.WriteLine("num1!=num2: " + (num1 != num2));
            Console.WriteLine(); //for space

            //logical operators
            Console.WriteLine("Logical operations:");
            if (isAvailable && num1 > num2)
            {
                Console.WriteLine("Item is available and num1 is greater than num2");
            }
            else if (isAvailable || num1 < num2)
            {
                Console.WriteLine("Either item is available or num1 is smaller");
            }
            else
            {
                Console.WriteLine("Condition not matched");
            }

            //printing other variables
            Console.WriteLine(); //for space
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Grade: " + grade);
            Console.WriteLine("Price: " + price);
            Console.WriteLine("\nPress any key to exit..");
            Console.ReadKey();//for pausing the console
        }
    }
}