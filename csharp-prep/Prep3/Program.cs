/* Travis Scoville (c) 2024
 * C# Prep 3
 */
using System;

class Program
{
    static void Main(string[] args)
    {
        Random rng = new();
        int magicNumber = rng.Next(1, 101);
        int guess = -1;
        int guesses = 0;

        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());
            guesses += 1;

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }

        Console.WriteLine($"It took you {guesses} guesses.");
    }
}