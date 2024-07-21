using System.Xml;

class Until : Expression
{
    public Until(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }

    public override Value Evaluate()
    {
        Value result = new Value();
        while (!_right.Evaluate().IsTruthy())
        {
            result = _left.Evaluate();
            
        }

        return result;
    }
}