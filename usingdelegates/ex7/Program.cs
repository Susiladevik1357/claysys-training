using System;
using System.ComponentModel.Design;

//define delegate
delegate int MathOperation(int a, int b);
class Program
{
    static int Add(int a, int b)
    {
        return a + b;
    }
    static int Subtract(int a, int b)
    {
        return a-b;
    }
    static int Multiply(int a, int b)
    {
        return a * b;
    }
    static int Divide(int a, int b)
    {
        if (b != 0)
        {
            return a/ b;
        }
        else
        {
            Console.WriteLine("Division by zero is not allowed.");
            return 0;
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("===Math Operations==\n");

        MathOperation operation;
        Console.Write("Enter first number: ");
        int num1 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter second number: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        operation = Add;
        Console.WriteLine($"\nAddition Result: {operation(num1, num2)}");
        operation = Subtract;
        Console.WriteLine($"\nSubtraction Result: {operation(num1, num2)}");
        operation = Multiply;
        Console.WriteLine($"\nMultiply Result: {operation(num1, num2)}");
        operation = Divide;
        Console.WriteLine($"\nDivision Result: {operation(num1, num2)}");

        Console.WriteLine("\nProgran completed successfully!");
    }
}