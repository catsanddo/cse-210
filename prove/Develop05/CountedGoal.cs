class CountedGoal : Goal
{
	private int _stepValue;
	private int _steps;
	private int _totalSteps;

	public CountedGoal(string description, int value, int stepValue, int totalSteps) : base(description, value)
	{
		_stepValue = stepValue;
		_steps = 0;
		_totalSteps = totalSteps;
	}
	
	public CountedGoal(string description, int value, int stepValue, int steps, int totalSteps) : base(description, value)
	{
		_stepValue = stepValue;
		_steps = steps;
		_totalSteps = totalSteps;
	}

	public override int Mark()
	{
		int result = 0;
		if (_steps < _totalSteps)
		{
			_steps += 1;
			result = _stepValue;
			if (_steps == _totalSteps)
			{
				result += _value;
			}
		}
		else
		{
			Console.WriteLine("You already completed this goal.");
		}

		return result;
	}

	public override void Display()
	{
		Console.WriteLine($"[{_steps}/{_totalSteps}] {_description} ({_stepValue}/{_value})");
	}
}
