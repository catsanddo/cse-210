/* Travis Scoville (c) 2024
 * Journal program main
 */
using System;

class Program
{
    static void DisplayMenu()
    {
        Console.WriteLine("Menu options:");
        Console.WriteLine("\t1. Write Entry");
        Console.WriteLine("\t2. Display Entries");
        Console.WriteLine("\t3. Load");
        Console.WriteLine("\t4. Save");
        Console.WriteLine("\t5. Quit");
    }

    static void Main(string[] args)
    {
        Journal journal = new();

        // Print header message
        Console.WriteLine("------------------");
        Console.WriteLine("Journal App");
        Console.WriteLine("By Travis Scoville");
        Console.WriteLine("------------------\n");

        bool running = true;
        while (running)
        {
            DisplayMenu();

            Console.Write("> ");
            string rawUserChoice = Console.ReadLine();
            int userChoice;
            if (!int.TryParse(rawUserChoice, out userChoice))
            {
                userChoice = -1;
            }

            Console.WriteLine();

            switch (userChoice)
            {
                case 1:
                    journal.CreateEntry();
                    break;
                case 2:
                    journal.Display();
                    break;
                case 3:
                    journal.Load();
                    break;
                case 4:
                    journal.Save();
                    break;
                case 5:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Please choose one of the provided menu options.");
                    break;
            }
        }
    }
}