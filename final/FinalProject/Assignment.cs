class Assignment : Expression
{
    private string _symbol;
    private State _state;
    
    public Assignment(string symbol, Expression operand, State state)
    {
        _symbol = symbol;
        _left = operand;
        _state = state;
    }

    public override Value Evaluate()
    {
        Value value = _left.Evaluate();
        _state.SetValue(_symbol, value);
        return value;
    }
}
