abstract class Expression
{
    Expression _left;
    Expression _right;

    public abstract Value Evaluate();
}
