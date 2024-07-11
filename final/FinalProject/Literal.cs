// TODO: This class probably either requires renaming or an expansion
class Literal : Expression
{
    private double _value;

    public Literal(double value)
    {
        _value = value;
    }

    public override Value Evaluate()
    {
        return new Value(_value);
    }
}
