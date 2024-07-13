class Division : Expression
{
    public Division(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();
        if (right.GetNumber() == 0)
        {
            throw new RuntimeException($"Division by zero.");
        }
        if (left.Type == ValueType.Number && right.Type == ValueType.Number)
        {
            return new Value((double)left.GetNumber() / (double)right.GetNumber());
        }
        throw new RuntimeException($"Unsupported operation '/' on {left.Type} and {right.Type}.");
    }
}
