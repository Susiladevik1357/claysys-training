using System;
using System.Collections.Generic;

namespace StackExample
{
    // Generic Stack class
    public class Stack<T>
    {
        private List<T> elements = new List<T>();

        // Method to add an item to the stack
        public void Push(T item)
        {
            elements.Add(item);
            Console.WriteLine($"{item} pushed to stack.");
        }

        // Method to remove the top item from the stack
        public T Pop()
        {
            if (elements.Count == 0)
            {
                Console.WriteLine("Stack is empty. Cannot pop.");
                return default(T); // returns default value like null or 0
            }
            T item = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            Console.WriteLine($"{item} popped from stack.");
            return item;
        }

        // Method to view the top item without removing it
        public T Peek()
        {
            if (elements.Count == 0)
            {
                Console.WriteLine("Stack is empty. Nothing to peek.");
                return default(T);
            }
            return elements[elements.Count - 1];
        }
    }

    // Main class
    class Program
    {
        static void Main(string[] args)
        {
            // Stack of strings (student names)
            Stack<string> nameStack = new Stack<string>();
            Console.WriteLine("Enter  names (type 'end' to stop):");

            while (true)
            {
                string name = Console.ReadLine();
                if (name.ToLower() == "end")
                    break;
                nameStack.Push(name);
            }

            Console.WriteLine("\nTop of stack: " + nameStack.Peek());

            nameStack.Pop();
            Console.WriteLine("After popping, top of stack: " + nameStack.Peek());

            // Test with integers
            Console.WriteLine("\nTesting with integers:");
            Stack<int> numberStack = new Stack<int>();
            numberStack.Push(10);
            numberStack.Push(20);
            numberStack.Push(30);

            Console.WriteLine("Top of integer stack: " + numberStack.Peek());
            numberStack.Pop();
            Console.WriteLine("After popping, top of integer stack: " + numberStack.Peek());

        }
    }
}
