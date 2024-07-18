class EqualTo : Expression
{
    public EqualTo(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        return new Value(_left.Evaluate().Equals(_right.Evaluate()) ? 1 : 0);
    }
}
