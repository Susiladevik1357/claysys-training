using System;
class Program
{
    static int DivideNumbers(int a, int b)
    {
        int result = 0;
        try
        {
            result = a / b;
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: you cannot divide by zero!");
        }
        return result;
    }
    static void Main()
    {
        Console.WriteLine("=== Division with Exception Handling ===");
        try
        {
            Console.Write("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Second number: ");
            int b = Convert.ToInt32(Console.ReadLine());
            int result = DivideNumbers(a, b);
            Console.WriteLine("Resulat: " + result);
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter valid numbers only!");
        }
    }
}