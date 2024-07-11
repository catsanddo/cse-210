class Negation : Expression
{
    public Negation(Expression operand)
    {
        _left = operand;
    }

    // TODO: consider other types
    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        if (left.Type == ValueType.Number)
        {
            return new Value(-(double)(double)(double)left.GetNumber());
        }
        return new Value();
    }
}
