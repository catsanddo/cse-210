class LogicalNot : Expression
{
    public LogicalNot(Expression operand)
    {
        _left = operand;
    }

    public override Value Evaluate()
    {
        Value result = _left.Evaluate();
        if (result.IsTruthy())
        {
            return new Value(0);
        }
        return new Value(1);
    }
}
