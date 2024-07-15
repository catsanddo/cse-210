class Assignment : Expression
{
    public Assignment(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        if (!(_left is Index) && !(_left is Identifier))
        {
            throw new RuntimeException($"Cannot assign to an rvalue.");
        }
        Value value = _right.Evaluate();
        return _left.Assign(value);
    }
}
