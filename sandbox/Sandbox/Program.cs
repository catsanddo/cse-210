using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Do you like C sharp? ");
        string answer = Console.ReadLine();

        if (answer == "yes") {
            Console.WriteLine("Me too.");
        } else if (answer == "no") {
            Console.WriteLine("Too bad.");
        } else {
            Console.WriteLine("Huh?");
        }
        
        Console.WriteLine("Goodbye.");
    }
}