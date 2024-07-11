class LogicalAnd : Expression
{
    public LogicalAnd(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    // TODO: what values are considered truthy or falsey?
    public override Value Evaluate()
    {
        Value leftValue = _left.Evaluate();
        if (leftValue.Type == ValueType.Number && leftValue.GetNumber() != 0)
        {
            return _right.Evaluate();
        }

        return leftValue;
    }
}
