using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Parser parser = new Parser(null);
        
        if (args.Length != 0 && File.Exists(args[0]))
        {
            parser = new Parser(File.OpenText(args[0]));
            Value result = parser.Parse();
            Console.WriteLine(result);
            return;
        }

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            
            parser.FeedLine(input);
            Value result = parser.Parse();
            Console.WriteLine($"  -> {result}");
        }
    }
}
