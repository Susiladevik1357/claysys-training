using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();
        Console.WriteLine("Evem numbers in the list:");
        foreach (var num in evenNumbers)
        {
            Console.Write(num + " ");
        }
        Func<int, int> square = x => x * x;
        int result = square(5);
        Console.WriteLine("\n\nSquare of 5 is: " + result);
    }
}