class LogicalOr : Expression
{
    public LogicalOr(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        if (left.IsTruthy())
        {
            return new Value(1);
        }
        return new Value(_right.Evaluate().IsTruthy() ? 1 : 0);
    }
}