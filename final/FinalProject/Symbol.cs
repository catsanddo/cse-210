class Symbol : Expression
{
    private string _name;
    private State _state;

    public Symbol(string name, State state)
    {
        _name = name;
        _state = state;
    }

    public override Value Evaluate()
    {
        return _state.GetValue(_name);
    }
}
