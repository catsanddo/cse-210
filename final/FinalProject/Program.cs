using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 0 && File.Exists(args[0]))
        {
            Scanner scanner = new Scanner(File.OpenText(args[0]));
            Token token;
            while ((token = scanner.GetToken()) != null)
            {
                Console.WriteLine(token.GetLexeme());
            }
            return;
        }
        
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            
            Scanner scanner = new Scanner(input);
            Token token;
            while ((token = scanner.GetToken()) != null)
            {
                Console.Write("\t");
                Console.WriteLine(token.GetLexeme());
            }
        }
    }
}
