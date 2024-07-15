class Identifier : Expression
{
    private State _state;
    private string _name;

    public Identifier(string name, State state)
    {
        _state = state;
        _name = name;
    }

    public override Value Evaluate()
    {
        return _state.GetValue(_name);
    }

    public override Value Assign(Value value)
    {
        _state.SetValue(_name, value);
        return value;
    }
}
