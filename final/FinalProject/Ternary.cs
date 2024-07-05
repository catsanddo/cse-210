class Ternary : Expression
{
    Expression _condition;

    public Ternary(Expression condition, Expression left, Expression right)
    {
        _condition = condition;
        _left = left;
        _right = right;
    }

    public override double Evaluate()
    {
        if ((int)_condition.Evaluate() == 0)
        {
            return _right.Evaluate();
        }

        return _left.Evaluate();
    }
}
