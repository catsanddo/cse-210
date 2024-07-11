abstract class Expression
{
    protected Expression _left;
    protected Expression _right;
    
    public abstract Value Evaluate();
}
