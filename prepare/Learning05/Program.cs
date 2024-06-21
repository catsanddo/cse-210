using System;

class Program
{
    static void Main(string[] args)
    {
        Square square = new("green", 5);
        Rectangle rect = new("red", 2, 8);
        Circle circle = new("blue", 5);

        List<Shape> shapes = new();
        shapes.Add(square);
        shapes.Add(rect);
        shapes.Add(circle);

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"The shape is {shape.GetColor()}.");
            Console.WriteLine($"It has an area of {shape.GetArea()}.");
            Console.WriteLine();
        }
    }
}