class Power : Expression
{
    public Power(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override double Evaluate()
    {
        return Math.Pow(_left.Evaluate(), _right.Evaluate());
    }
}
