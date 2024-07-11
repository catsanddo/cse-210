class Repeat : Expression
{
    public Repeat(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value result = new Value();
        Value condition;
        while ((condition = _right.Evaluate()).GetNumber() == 0)
        {
            result = _left.Evaluate();
        }

        return result;
    }
}
