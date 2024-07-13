class Literal : Expression
{
    private Value _value;

    public Literal(Value value)
    {
        _value = value;
    }

    public override Value Evaluate()
    {
        return _value;
    }
}
