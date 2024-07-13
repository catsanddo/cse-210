class Append : Expression
{
    public Append(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();
        if (left.Type == ValueType.Array)
        {
            if (right.Type == ValueType.Array)
            {
                return new Value(left.GetArray().Concat(right.GetArray()).ToArray());
            }
            if (right.Type == ValueType.Number)
            {
                return new Value(left.GetArray().Append((double)right.GetNumber()).ToArray());
            }
        }
        else if (left.Type == ValueType.Number)
        {
            double[] first = new double[1];
            first[0] = (double)left.GetNumber();
            if (right.Type == ValueType.Array)
            {
                return new Value(first.Concat(right.GetArray()).ToArray());
            }
            if (right.Type == ValueType.Number)
            {
                return new Value(first.Append((double)right.GetNumber()).ToArray());
            }
        }
        throw new RuntimeException($"Unsupported operation '<<' on {left.Type} and {right.Type}.");
    }
}
