class Ternary : Expression
{
    private Expression _condition;

    public Ternary(Expression condition, Expression left, Expression right)
    {
        _condition = condition;
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        if (_condition.Evaluate().IsTruthy())
        {
            return _left.Evaluate();
        }
        return _right.Evaluate();
    }
}
