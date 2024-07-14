class Subtraction : Expression
{
    public Subtraction(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();
        if (left.Type == ValueType.Number && right.Type == ValueType.Number)
        {
            return new Value((double)left.GetNumber() - (double)right.GetNumber());
        }
        if (left.Type == ValueType.Array && right.Type == ValueType.Array)
        {
            double[] lhs = left.GetArray();
            double[] rhs = right.GetArray();
            if (lhs.Length != rhs.Length)
            {
                throw new RuntimeException($"Cannot subtract arrays of different lengths.");
            }
            for (int i = 0; i < lhs.Length; ++i)
            {
                lhs[i] -= rhs[i];
            }
            return new Value(lhs);
        }
        throw new RuntimeException($"Unsupported operation '-' on {left.Type} and {right.Type}.");
    }
}
