using System.IO;

class UserProfile
{
	private List<Goal> _goals = [];
	private int _points = 0;

	public void Save(string filePath)
	{
		using (StreamWriter sw = File.CreateText(filePath))
		{
			sw.WriteLine("[UserProfile]");
			sw.WriteLine($"points = {_points}");

			foreach (Goal goal in _goals)
			{
				sw.Write(goal.Serialize());
			}
		}
	}

	public bool Load(string filePath)
	{
		if (!File.Exists(filePath))
		{
			return false;
		}

		_goals.Clear();

		using (StreamReader sr = File.OpenText(filePath))
		{
			string state = "newline";
			string objectName = "";
			string key = "";
			string value = "";
			Dictionary<string, string> body = new();

			int c;
			while ((c = sr.Read()) >= 0)
			{
				if (state == "newline")
				{
					if (c == '[')
					{
						state = "object";
						// TODO: process previous object
						if (objectName == "UserProfile")
						{
							bool success = true;
							string rawPoints = "";
							success = success && body.TryGetValue("points", out rawPoints);

							// This will set _points if successful
							if (!success || !int.TryParse(rawPoints, out _points))
							{
								continue;
							}
						}
						else if (objectName == "SimpleGoal")
						{
							bool success = true;
							string description = "";
							string rawPValue = "";
							int pValue = 0;
							string isComplete = "";
							success = success && body.TryGetValue("description", out description);
							success = success && body.TryGetValue("value", out rawPValue);
							success = success && body.TryGetValue("is-complete", out isComplete);
							success = success && int.TryParse(rawPValue, out pValue);
							
							if (!success)
							{
								continue;
							}

							SimpleGoal goal = new(description, pValue, isComplete == "True");
							_goals.Add(goal);
						}
						else if (objectName == "RepeatedGoal")
						{
							bool success = true;
							string description = "";
							string rawPValue = "";
							int pValue = 0;
							string rawCount = "";
							int count = 0;
							success = success && body.TryGetValue("description", out description);
							success = success && body.TryGetValue("value", out rawPValue);
							success = success && body.TryGetValue("count", out rawCount);
							success = success && int.TryParse(rawPValue, out pValue);
							success = success && int.TryParse(rawCount, out count);
							
							if (!success)
							{
								continue;
							}

							RepeatedGoal goal = new(description, pValue, count);
							_goals.Add(goal);
						}
						else if (objectName == "CountedGoal")
						{
							bool success = true;
							string description = "";
							string rawPValue = "";
							int pValue = 0;
							string rawStepValue = "";
							int stepValue = 0;
							string rawSteps = "";
							int steps = 0;
							string rawTotalSteps = "";
							int totalSteps = 0;
							success = success && body.TryGetValue("description", out description);
							success = success && body.TryGetValue("value", out rawPValue);
							success = success && body.TryGetValue("step-value", out rawStepValue);
							success = success && body.TryGetValue("steps", out rawSteps);
							success = success && body.TryGetValue("total-steps", out rawTotalSteps);
							success = success && int.TryParse(rawPValue, out pValue);
							success = success && int.TryParse(rawStepValue, out stepValue);
							success = success && int.TryParse(rawSteps, out steps);
							success = success && int.TryParse(rawTotalSteps, out totalSteps);
							
							if (!success)
							{
								continue;
							}

							CountedGoal goal = new(description, pValue, stepValue, steps, totalSteps);
							_goals.Add(goal);
						}
						objectName = "";
					}
					else if (c != '\r')
					{
						key += (char)c;
						state = "key";
					}
				}
				else if (state == "object")
				{
					if (c == ']')
					{
						state = "skip";
						body = new();
					}
					else
					{
						objectName += (char)c;
					}
				}
				else if (state == "key")
				{
					if (c == '=')
					{
						key = key.Trim();
						state = "value";
					}
					else
					{
						key += (char)c;
					}
				}
				else if (state == "value")
				{
					if (c == '\r')
					{
						value = value.Trim();
						body[key] = value;
						state = "crlf";
						key = "";
						value = "";
					}
					else
					{
						value += (char)c;
					}
				}
				else if (state == "skip")
				{
					if (c == '\r')
					{
						state = "crlf";
					}
				}
				else if (state == "crlf")
				{
					if (c == '\n')
					{
						state = "newline";
					}
				}
			}
		
			if (objectName == "UserProfile")
			{
				bool success = true;
				string rawPoints = "";
				success = success && body.TryGetValue("points", out rawPoints);

				// This will set _points if successful
				if (success)
				{
					int.TryParse(rawPoints, out _points);
				}
			}
			else if (objectName == "SimpleGoal")
			{
				bool success = true;
				string description = "";
				string rawPValue = "";
				int pValue = 0;
				string isComplete = "";
				success = success && body.TryGetValue("description", out description);
				success = success && body.TryGetValue("value", out rawPValue);
				success = success && body.TryGetValue("is-complete", out isComplete);
				success = success && int.TryParse(rawPValue, out pValue);
		
				if (success)
				{
					SimpleGoal goal = new(description, pValue, isComplete == "True");
					_goals.Add(goal);
				}
			}
			else if (objectName == "RepeatedGoal")
			{
				bool success = true;
				string description = "";
				string rawPValue = "";
				int pValue = 0;
				string rawCount = "";
				int count = 0;
				success = success && body.TryGetValue("description", out description);
				success = success && body.TryGetValue("value", out rawPValue);
				success = success && body.TryGetValue("count", out rawCount);
				success = success && int.TryParse(rawPValue, out pValue);
				success = success && int.TryParse(rawCount, out count);
		
				if (success)
				{
					RepeatedGoal goal = new(description, pValue, count);
					_goals.Add(goal);
				}
			}
			else if (objectName == "CountedGoal")
			{
				bool success = true;
				string description = "";
				string rawPValue = "";
				int pValue = 0;
				string rawStepValue = "";
				int stepValue = 0;
				string rawSteps = "";
				int steps = 0;
				string rawTotalSteps = "";
				int totalSteps = 0;
				success = success && body.TryGetValue("description", out description);
				success = success && body.TryGetValue("value", out rawPValue);
				success = success && body.TryGetValue("step-value", out rawStepValue);
				success = success && body.TryGetValue("steps", out rawSteps);
				success = success && body.TryGetValue("total-steps", out rawTotalSteps);
				success = success && int.TryParse(rawPValue, out pValue);
				success = success && int.TryParse(rawStepValue, out stepValue);
				success = success && int.TryParse(rawSteps, out steps);
				success = success && int.TryParse(rawTotalSteps, out totalSteps);
		
				if (success)
				{
					CountedGoal goal = new(description, pValue, stepValue, steps, totalSteps);
					_goals.Add(goal);
				}
			}
		}

		return true;
	}

	public void CreateGoal()
	{
        int userChoice = Program.MenuSelect("What kind of goal would you like to create?", [
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

	private void CreateSimpleGoal()
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
	
	private void CreateRepeatedGoal()
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
	
	private void CreateCountedGoal()
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