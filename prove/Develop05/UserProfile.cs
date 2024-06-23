class UserProfile
{
	private List<Goal> _goals = [];
	private int _points = 0;

	public void Save(string filePath)
	{
		
	}

	public void Load(string filePath)
	{
		
	}

	public void CreateGoal()
	{
		Console.WriteLine("What kind of goal would you like to create?");
        int userChoice = Program.MenuSelect([
			"1. Simple Goal",
			"2. Repeated Goal",
			"3. Counted Goal",
        ]);

		if (userChoice == 1)
		{
			CreateSimpleGoal();
		}
		else if (userChoice == 2)
		{
			CreateRepeatedGoal();
		}
		else if (userChoice == 3)
		{
			CreateCountedGoal();
		}
	}

	public void CreateSimpleGoal()
	{
		Console.Write("What goal would you like to set? ");
		string description = Console.ReadLine();
		int value = Program.GetNumberInput("Please give it a point value: ", 1);

		SimpleGoal goal = new(description, value);

		Console.Write("Simple Goal: ");
		goal.Display();
		Console.Write("Create this Goal? (Y/n) ");
		string confirm = Console.ReadLine();

		if (confirm.ToLower() != "no" && confirm.ToLower() != "n")
		{
			_goals.Add(goal);
			Console.WriteLine("Goal was created successfully!");
		}
		else
		{
			Console.WriteLine("Goal creation aborted!");
		}
	}
	
	public void CreateRepeatedGoal()
	{
		Console.Write("What goal would you like to set? ");
		string description = Console.ReadLine();
		int value = Program.GetNumberInput("Please give it a point value: ", 1);

		RepeatedGoal goal = new(description, value);

		Console.Write("Repeated Goal: ");
		goal.Display();
		Console.Write("Create this Goal? (Y/n) ");
		string confirm = Console.ReadLine();

		if (confirm.ToLower() != "no" && confirm.ToLower() != "n")
		{
			_goals.Add(goal);
			Console.WriteLine("Goal was created successfully!");
		}
		else
		{
			Console.WriteLine("Goal creation aborted!");
		}
	}
	
	public void CreateCountedGoal()
	{
		Console.Write("What goal would you like to set? ");
		string description = Console.ReadLine();
		int value = Program.GetNumberInput("Please give it a point value: ", 1);
		int totalSteps = Program.GetNumberInput("How many times do you want to repeat this goal? ", 1);
		int stepValue = Program.GetNumberInput("Please give it point value for each step: ");

		CountedGoal goal = new(description, value, stepValue, totalSteps);

		Console.Write("Counted Goal: ");
		goal.Display();
		Console.Write("Create this Goal? (Y/n) ");
		string confirm = Console.ReadLine();

		if (confirm.ToLower() != "no" && confirm.ToLower() != "n")
		{
			_goals.Add(goal);
			Console.WriteLine("Goal was created successfully!");
		}
		else
		{
			Console.WriteLine("Goal creation aborted!");
		}
	}

	public void Display()
	{
		Console.WriteLine($"Total points: {_points}");
	}

	public void ListGoals()
	{
		if (_goals.Count == 0)
		{
			Console.WriteLine("You have not yet created any goals.");
			return;
		}
		
		Console.WriteLine("Goals:");
		int goalID = 1;
		foreach (Goal goal in _goals)
		{
			Console.Write($"\t{goalID}. ");
			goal.Display();
			goalID += 1;
		}
	}

	public void MarkGoal()
	{
		if (_goals.Count == 0)
		{
			Console.WriteLine("You have not yet created any goals.");
			return;
		}

		int goalID = Program.GetNumberInput("Enter the goal's ID: ", 1, _goals.Count);
		
		Goal goal = _goals[goalID-1];
		_points += goal.Mark();

		Console.Write("\t");
		goal.Display();
	}
}