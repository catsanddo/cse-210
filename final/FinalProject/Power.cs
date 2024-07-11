class Power : Expression
{
    public Power(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    // TODO: consider other types
    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();
        if (left.Type == ValueType.Number && right.Type == ValueType.Number)
        {
            return new Value(Math.Pow((double)left.GetNumber(), (double)right.GetNumber()));
        }
        return new Value();
    }
}
