using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        int percentage = int.Parse(Console.ReadLine());

        string letter;
        if (percentage < 60)
        {
            letter = "F";
        }
        else if (percentage < 70)
        {
            letter = "D";
        }
        else if (percentage < 80)
        {
            letter = "C";
        }
        else if (percentage < 90)
        {
            letter = "B";
        }
        else
        {
            letter = "A";
        }

        Console.WriteLine($"Your letter grade is: {letter}");

        if (percentage >= 70)
        {
            Console.WriteLine("You passed! Well done.");
        }
        else
        {
            Console.WriteLine("You didn't pass. Better luck next time.");
        }
    }
}