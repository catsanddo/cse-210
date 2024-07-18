class LessThan : Expression
{
    public LessThan(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        return new Value( _left.Evaluate().LessThan(_right.Evaluate()) ? 1 : 0);
    }
}
