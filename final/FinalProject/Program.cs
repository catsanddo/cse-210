using System;

class Program
{
    static void Main(string[] args)
    {
        // Global persistent state for the REPL
        State state = new();
        
        bool running = true;
        while (running)
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            Scanner scanner = new(input);
            Token[] tokens = scanner.Scan();

            if (tokens == null)
            {
                continue;
            }
            
            Parser parser = new(tokens, state);
            Value result;
            if (!parser.Parse(out result))
            {
                continue;
            }
            Console.WriteLine($"\t= {result}");
        }
    }
}