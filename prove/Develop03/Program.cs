using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new("Alma", 38, 15);
        string text = "And may the Lord bless your soul, and receive you at the last day into his kingdom, to sit down in peace. Now go, my son, and teach the word unto this people. Be sober. My son, farewell.";

        Scripture scripture = new(reference, text);

        bool running = true;
        while (running)
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine("\nPress enter to continue or type \"back\" or \"quit\"...");
            string response = Console.ReadLine();

            if (scripture.IsAllHidden())
            {
                running = false;
            }
            else if (response == "back")
            {
                scripture.UnhideWords();
            }
            else
            {
                scripture.HideWords();
            }

            running = running && response != "quit";
        }
    }
}