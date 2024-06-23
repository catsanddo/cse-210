class SimpleGoal : Goal
{
	private bool _isCompleted;

	public SimpleGoal(string description, int value) : base(description, value)
	{
		_isCompleted = false;
	}

	public SimpleGoal(string description, int value, bool isCompleted) : base(description, value)
	{
		_isCompleted = isCompleted;
	}

	public override int Mark()
	{
		if (_isCompleted)
		{
			Console.WriteLine("You already completed this goal.");
			return 0;
		}

		_isCompleted = true;
		return _value;
	}

	public override void Display()
	{
		string mark = _isCompleted ? "X" : " ";
		Console.WriteLine($"[{mark}] {_description} ({_value})");
	}
}
