using System;

class Program
{
    static void Main(string[] args)
    {
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
            
            Parser parser = new(tokens);
            Console.WriteLine($"\t= {parser.Parse()}");
        }
    }
}