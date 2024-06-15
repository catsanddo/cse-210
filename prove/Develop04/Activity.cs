using System;
using System.Threading;

class Activity
{
	private string _name;
	private string _description;
	private int _duration;
	private DateTime _startTime;
	protected Random _rand = new();

	public Activity(string name, string description)
	{
		_name = name;
		_description = description;
	}

	public void StartActivity()
	{
		Console.WriteLine($"Welcome to the {_name}!");
		Console.WriteLine(_description);
		Console.WriteLine();

		int duration = 0;
		while (duration <= 0)
		{
			Console.Write("How long would you like to do this activity (seconds)? ");
			string rawDuration = Console.ReadLine();

			if (!int.TryParse(rawDuration, out duration))
			{
				Console.WriteLine("Please input a valid number above 0.");
			}
		}
		Console.WriteLine();

		_duration = duration;
	}

	public void StartTimer()
	{
		_startTime = DateTime.Now;
	}

	public bool IsTimerExpired()
	{
		if (DateTime.Now >= _startTime.AddSeconds((double) _duration))
		{
			return true;
		}

		return false;
	}

	public void EndActivity()
	{
		Console.WriteLine("Great job!");
		Wait(3);
		Console.WriteLine($"You completed {_duration} seconds of the {_name}.");
		Wait(3);
	}

	public void Wait(int duration)
	{
		string eraser = "\b\b\b\b";
		string[] loadingBars = [
			"....",
			"o...",
			".o..",
			"..o.",
			"...o",
		];
		DateTime endTime = DateTime.Now.AddSeconds((double) duration);

		int index = 0;
		while (DateTime.Now < endTime)
		{
			Console.Write(loadingBars[index]);
			index += 1;
			if (index >= loadingBars.Count())
			{
				index = 0;
			}

			Thread.Sleep(500);
			Console.Write(eraser);
		}

		Console.Write("    ");
		Console.Write(eraser);
	}

	protected string PickItem(string[] items)
	{
		int index = _rand.Next(items.Count());
		return items[index];
	}
}
