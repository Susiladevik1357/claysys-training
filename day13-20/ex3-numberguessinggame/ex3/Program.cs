using System;
namespace NumberGuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //create random number generator
            Random random = new Random();
            
            //create random num between 1to100
            int target = random.Next(1, 101);
            
            //create variable to store user guess
            int guess = 0;
            Console.WriteLine("Welcome to the Number Guessing Game!");
            Console.WriteLine("I'm thinking of a number between 1 and 100.");
            
            //use a while loop to keep asking user for guess until they guess correctly
            while (guess != target)
            {
                Console.WriteLine("\nEnter your guess: ");
                
                //get user input and convert to an int
                guess = Convert.ToInt32(Console.ReadLine());
                
                //check the guess using if-else statement
                if (guess > target)
                {
                    Console.WriteLine("Too high! try again.");
                }
                else if (guess < target)
                {
                    Console.WriteLine("Too low! try again.");
                }
                else
                {
                    Console.WriteLine("Congratulations! you guessed the number!");
                }
            }
            
            //message to end the game
            Console.WriteLine("\nThank you for playing!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}