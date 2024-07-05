class Division : Expression
{
    public Division(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override double Evaluate()
    {
        return _left.Evaluate() / _right.Evaluate();
    }
}
