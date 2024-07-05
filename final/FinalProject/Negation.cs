class Negation : Expression
{
    public Negation(Expression operand)
    {
        _left = operand;
    }

    public override double Evaluate()
    {
        return -_left.Evaluate();
    }
}
