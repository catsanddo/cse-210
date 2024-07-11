class GreaterThan : Expression
{
    public GreaterThan(Expression left, Expression right)
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
            return new Value(left.GetNumber() > right.GetNumber() ? 1 : 0);
        }
        return new Value();
    }
}
