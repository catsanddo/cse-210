class Negation : Expression
{
    public Negation(Expression operand)
    {
        _left = operand;
    }

    public override Value Evaluate()
    {
        Value result = _left.Evaluate();
        if (result.Type == ValueType.Number)
        {
            return new Value(-(double)result.GetNumber());
        }
        if (result.Type == ValueType.Array)
        {
            double[] array = result.GetArray();
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = -array[i];
            }
            return result;
        }
        throw new RuntimeException($"Unsupported operation '-' on {result.Type}.");
    }
}
