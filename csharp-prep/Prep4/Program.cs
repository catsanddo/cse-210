/* Travis Scoville (c) 2024
 * C# Prep 4
 */
using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = [];

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int newNumber = 1;
        while (newNumber != 0)
        {
            Console.Write("Enter a number: ");
            newNumber = int.Parse(Console.ReadLine());

            if (newNumber != 0)
            {
                numbers.Add(newNumber);
            }
        }

        int largest = numbers.First();
        int smallest = numbers.First();
        int sum = 0;

        foreach (int element in numbers)
        {
            if (element > largest)
            {
                largest = element;
            }
            if (element > 0 && element < smallest)
            {
                smallest = element;
            }

            sum += element;
        }

        double average = sum;
        average /= (double) numbers.Count;
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average:G}");
        Console.WriteLine($"The largest number is: {largest}");
        Console.WriteLine($"The smallest positive number is: {smallest}");
    }
}