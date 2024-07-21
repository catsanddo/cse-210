class LogicalAnd : Expression
{
    public LogicalAnd(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        if (left.IsTruthy())
        {
            return new Value(_right.Evaluate().IsTruthy() ? 1 : 0);
        }
        return new Value((double)0);
    }
}