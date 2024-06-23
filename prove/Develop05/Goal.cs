abstract class Goal
{
	protected string _description;
	protected int _value;

	public Goal(string description, int value)
	{
		_description = description;
		_value = value;
	}

	public abstract int Mark();
	public abstract void Display();
}
