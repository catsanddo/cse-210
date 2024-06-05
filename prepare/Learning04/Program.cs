using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment assignment = new("Bob Jones", "Geology");
        Console.WriteLine(assignment.GetSummary());

        Console.WriteLine();

        MathAssignment math = new("Jeremy Stones", "Factoring", "5.3", "4-14");
        Console.WriteLine(math.GetSummary());
        Console.WriteLine(math.GetHomeworkList());

        Console.WriteLine();

        WritingAssignment writing = new("Sarah Robertson", "Music Theory", "Properties of Tritones");
        Console.WriteLine(writing.GetSummary());
        Console.WriteLine(writing.GetWritingInformation());
    }
}