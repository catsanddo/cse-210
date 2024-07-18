class LessOrEqualTo : Expression
{
    public LessOrEqualTo(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value left = _left.Evaluate();
        Value right = _right.Evaluate();

        return new Value(left.LessThan(right) || left.Equals(right) ? 1 : 0);
    }
}
