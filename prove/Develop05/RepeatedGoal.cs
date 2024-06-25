class RepeatedGoal : Goal
{
	private int _count;

	public RepeatedGoal(string description, int value) : base(description, value)
	{
		_count = 0;
	}
	
	public RepeatedGoal(string description, int value, int count) : base(description, value)
	{
		_count = count;
	}

	public override int Mark()
	{
		_count += 1;

		return _value;
	}

	public override void Display()
	{
		Console.WriteLine($"[{_count}] {_description} ({_value})");
	}

	public override string Serialize()
	{
		string result = "[RepeatedGoal]\r\n";

		result += $"description = {_description}\r\n";
		result += $"value = {_value}\r\n";
		result += $"count = {_count}\r\n";

		return result;
	}
}
