using System;

class Player
{
    public string? Name;
    public int Health;

    public void DisplayInfo()
    {
        if (Name == null)
        {
            Console.WriteLine("Name: nameless");
        }
        else
        {
            Console.WriteLine($"Name: {Name}");
        }
        Console.WriteLine($"HP: {Health}");
    }
}

class Program
{
    static void Main(string []args)
    {
        Player player = new Player {Name = "Graham", Health = 20};
        player.DisplayInfo();
    }
}
