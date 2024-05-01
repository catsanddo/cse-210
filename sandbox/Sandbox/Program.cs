using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        for (bool running = true; running; )
        {
            Console.WriteLine("Hello, World!");

            Console.Write("Would you like to execute again? ");
            string answer = Console.ReadLine();

            if (answer != "y")
            {
                running = false;
            }
        }
    }
}