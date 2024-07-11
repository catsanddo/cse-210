class LogicalNot : Expression
{
    public LogicalNot(Expression operand)
    {
        _left = operand;
    }

    // TODO: what values are considered truthy or falsey?
    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        if (left.Type == ValueType.Number && left.GetNumber() == 0)
        {
            return new Value(1);
        }
        return new Value(0);
    }
}
