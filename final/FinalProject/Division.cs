class Division : Expression
{
    public Division(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    // TODO: other types
    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();
        if (left.Type == ValueType.Number && right.Type == ValueType.Number)
        {
            return new Value((double)left.GetNumber() / (double)right.GetNumber());
        }
        return new Value();
    }
}
