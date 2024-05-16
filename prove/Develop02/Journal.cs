/* Travis Scoville (c) 2024
 * Journal program main
 */
using System.IO;

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

    /* File format for saving journals is non-standard.
     * A plain-text file consisting of entries separated by a '.' alone on a line.
     * It is impossible to enter a '.' alone on a line, so this is safe. The
     * first line of the entry is the date string. The second line is the prompt.
     * All remaining lines make up the body of the response. The final entry should
     * be followed by a '.' alone on a line to signal its end.
     */

    // Overload for Save()
    // When invoked without arguments it queries the user for a filename
    public void Save()
    {
        Console.Write("Enter a file name: ");
        string path = Console.ReadLine();
        Save(path);
    }

    public void Save(string path)
    {
        Console.WriteLine("Saving entries...");

        // An empty path will cause WriteAllText to crash; early return
        if (path == "")
        {
            Console.WriteLine();
            return;
        }

        // No entries to save; early return
        if (_entries.Count < 1)
        {
            Console.WriteLine();
            return;
        }

        string stringyEntries = "";
        foreach (Entry entry in _entries)
        {
            stringyEntries += entry.Serialize() + "\n" + "." + "\n";
        }
        // Remove the last colon
        //stringyEntries = stringyEntries[..^2];

        File.WriteAllText(path, stringyEntries);
        Console.WriteLine();
    }

    // Overload for Load(); see Save()
    public void Load()
    {
        Console.Write("Enter a file name: ");
        string path = Console.ReadLine();
        Load(path);
    }

    public void Load(string path)
    {
        Console.WriteLine("Loading entries...");

        // File does not exist; early return
        if (!File.Exists(path))
        {
            Console.WriteLine($"File '{path}' does not exist!\n");
            return;
        }

        _entries.Clear();

        // Read and parse file line by line
        // state keeps track of the meaning of each line
        // it's just a mini state-machine
        StreamReader reader = new(path);
        string state = "date";
        Entry newEntry = new();
        for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            // In the event of an early end-of-entry
            // We will save a broken entry with fields containing empty strings
            // Maybe I will find a way to handle this later...
            if (line == ".")
            {
                state = "date";
                newEntry._response = newEntry._response.Trim();
                _entries.Add(newEntry);
                newEntry = new();
            }
            else if (state == "date")
            {
                newEntry._date = line;
                state = "prompt";
            }
            else if (state == "prompt")
            {
                newEntry._prompt = line;
                state = "response";
            }
            else
            {
                newEntry._response += line + "\n";
            }
        }
        Console.WriteLine();
    }
}