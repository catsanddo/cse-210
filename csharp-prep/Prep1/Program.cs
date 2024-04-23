using System;

class Program
{
    static void Main(string[] args)
    {
        string FirstName;
        string LastName;

        Console.Write("What is your first name? ");
        FirstName = Console.ReadLine();
        Console.Write("What is your last name? ");
        LastName = Console.ReadLine();

        Console.WriteLine($"Your name is {LastName}, {FirstName} {LastName}.");
    }
}