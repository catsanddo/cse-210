class Ternary : Expression
{
    Expression _condition;

    public Ternary(Expression condition, Expression left, Expression right)
    {
        _condition = condition;
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        if (_condition.Evaluate().GetNumber() == 0)
        {
            return _right.Evaluate();
        }

        return _left.Evaluate();
    }
}
