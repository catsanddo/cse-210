class Series : Expression
{
    public Series(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        _left.Evaluate();
        return _right.Evaluate();
    }
}
