class Index : Expression
{
    public Index(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();
        if (left.Type == ValueType.Array && right.Type == ValueType.Number)
        {
            double[] array = left.GetArray();
            int index = (int)right.GetNumber();
            if (index < 0 || index >= array.Length)
            {
                throw new RuntimeException($"Array access out of bounds ({index}).");
            }
            return new Value(array[index]);
        }
        throw new RuntimeException($"Unsupported operation '[]' on {left.Type} and {right.Type}.");
    }
}
