class SimpleGoal : Goal
{
	private bool _isComplete;

	public SimpleGoal(string description, int value) : base(description, value)
	{
		_isComplete = false;
	}

	public SimpleGoal(string description, int value, bool isComplete) : base(description, value)
	{
		_isComplete = isComplete;
	}

	public override int Mark()
	{
		if (_isComplete)
		{
			Console.WriteLine("You already completed this goal.");
			return 0;
		}

		_isComplete = true;
		return _value;
	}

	public override void Display()
	{
		string mark = _isComplete ? "X" : " ";
		Console.WriteLine($"[{mark}] {_description} ({_value})");
	}

	public override string Serialize()
	{
		string result = "[SimpleGoal]\r\n";

		result += $"description = {_description}\r\n";
		result += $"value = {_value}\r\n";
		result += $"is-complete = {_isComplete}\r\n";

		return result;
	}
}
