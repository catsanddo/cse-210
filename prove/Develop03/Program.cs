/* Travis Scoville (c) 2024
 * Scripture Memorizer
 * I exceeded the requirements in this assignment by providing a special "back"
 * command. If the user types "back" it will unhide the last three words. It
 * keeps track of the order words were hidden in so that only the three most
 * recent words will be revealed. It does NOT keep track of which words were
 * revealed so there is no "redo" functionality; three random words will be
 * revealed each time.
 *
 * I did this by removing data from the Word class. Each word no longer keeps
 * track of whether or not it was hidden. Instead the Scripture class maintains
 * an (ordered) list of "references" (or indexes) to words that are hidden. The
 * scripture class then passes this information back to each Word as they are
 * rendered so that they can properly decide whether to return their content or
 * a blank word.
 */
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

            if (response == "back")
            {
                scripture.UnhideWords();
            }
            else if (scripture.IsAllHidden())
            {
                running = false;
            }
            else
            {
                scripture.HideWords();
            }

            running = running && response != "quit";
        }
    }
}