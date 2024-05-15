/* Travis Scoville (c) 2024
 * Journal program main
 */

class Journal
{
    public List<Entry> _entries = [];
    public PromptGenerator _promptGen;

    public Journal()
    {
        _promptGen = new();
        _promptGen._rng = new();
    }

    public void Display()
    {
        Console.WriteLine("----------------------------------\n");
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry._date);
            Console.WriteLine(entry._prompt);
            Console.WriteLine();
            Console.WriteLine(entry._response);
            Console.WriteLine("----------------------------------\n");
        }
    }

    public void CreateEntry()
    {
        Entry newEntry = new();
        
        string prompt = _promptGen.PickPrompt();
        newEntry.TakeResponse(prompt);

        _entries.Add(newEntry);

        Console.WriteLine();
    }

    public void Save()
    {
        Console.WriteLine("Saving...\n");
    }

    public void Load()
    {
        Console.WriteLine("Loading...\n");
    }
}