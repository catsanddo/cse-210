using System;

/* I exceeded requirements in this activity by ensuring that no questions would be
 * repeated in the Reflection Activity until all questions had been asked first.
 */

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the mindfulness meditation app!");
        Console.WriteLine("Please choose an activity from the menu below.");

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Main Menu:");
            Console.WriteLine("  1. Breathing");
            Console.WriteLine("  2. Reflection");
            Console.WriteLine("  3. Listing");
            Console.WriteLine("  4. Quit");

            Console.Write("> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                BreathingActivity activity = new();
                activity.PerformActivity();
            }
            else if (input == "2")
            {
                ReflectionActivity activity = new();
                activity.PerformActivity();
            }
            else if (input == "3")
            {
                ListingActivity activity = new();
                activity.PerformActivity();
            }
            else if (input == "4")
            {
                running = false;
            }
            else
            {
                Console.WriteLine("Unrecognized menu option.\n");
            }
        }
    }
}