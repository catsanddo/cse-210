using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to your Goal Tracker!");

        UserProfile user = new();
        user.Load("user-data.ini");

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            int userChoice = MenuSelect("Main Menu:", [
                "1. View Profile",
                "2. List Goals",
                "3. Mark Goal",
                "4. Create Goal",
                "5. Manage Data",
                "6. Quit",
            ]);

            if (userChoice == 1)
            {
                user.Display();
            }
            else if (userChoice == 2)
            {
                user.ListGoals();
            }
            else if (userChoice == 3)
            {
                user.MarkGoal();
            }
            else if (userChoice == 4)
            {
                user.CreateGoal();
            }
            else if (userChoice == 5)
            {
                DataMenu(user);
            }
            else if (userChoice == 6)
            {
                running = false;
            }
        }

        user.Save("user-data.ini");
        Console.WriteLine("Goodbye!");
    }

    static private void DataMenu(UserProfile user)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.WriteLine();
            int userChoice = MenuSelect("Data Management Menu:", [
                "1. Export data",
                "2. Import data",
                "3. Exit",
            ]);

            if (userChoice == 1)
            {
                Console.Write("Enter the file to export to: ");
                string filePath = Console.ReadLine();
                user.Save(filePath);
            }
            else if (userChoice == 2)
            {
                Console.Write("Enter the file to import from: ");
                string filePath = Console.ReadLine();
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File `{filePath}` does not exist!");
                }
                user.Load(filePath);
            }
            else if (userChoice == 3)
            {
                inMenu = false;
            }
        }
    }
    
    static public int MenuSelect(string heading, string[] choices)
    {
        int choice = 0;

        if (choices.Count() == 0)
        {
            return 0;
        }

        while (choice < 1 || choice > choices.Count())
        {
            Console.WriteLine(heading);
            foreach (string option in choices)
            {
                Console.WriteLine($"\t{option}");
            }

            Console.Write("> ");
            string userChoice = Console.ReadLine();
            Console.WriteLine();

            bool isNumber = int.TryParse(userChoice, out choice);
            if (!isNumber || choice < 1 || choice > choices.Count())
            {
                Console.WriteLine("Invalid option!\n");
            }
        }

        return choice;
    }

    static public int GetNumberInput(string prompt, int? nMin = null, int? nMax = null)
    {
        bool inputIsValid = false;
        int number = 0;
        while (!inputIsValid)
        {
            Console.Write(prompt);
            string userInput = Console.ReadLine();

            bool isNumber = int.TryParse(userInput, out number);

            int min = nMin ?? number;
            int max = nMax ?? number;
            
            if (isNumber && number >= min && number <= max)
            {
                inputIsValid = true;
            }
            else
            {
                Console.WriteLine("Invalid number!");
            }

        }

        return number;
    }
}