class Literal : Expression
{
    private double _value;

    public Literal(double value)
    {
        _value = value;
    }

    public override double Evaluate()
    {
        return _value;
    }
}
