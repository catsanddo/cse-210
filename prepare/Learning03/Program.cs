using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction one = new();
        Fraction five = new(5);
        Fraction third = new(1, 3);
        Fraction improper = new(5, 4);

        improper.SetNumerator(7);
        Console.WriteLine(improper.GetNumerator());
        
        Console.WriteLine();
        Console.WriteLine(one.GetFractionString());
        Console.WriteLine(one.GetDecimalValue());
        Console.WriteLine(five.GetFractionString());
        Console.WriteLine(five.GetDecimalValue());
        Console.WriteLine(third.GetFractionString());
        Console.WriteLine(third.GetDecimalValue());
        Console.WriteLine(improper.GetFractionString());
        Console.WriteLine(improper.GetDecimalValue());
    }
}