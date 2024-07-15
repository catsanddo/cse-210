class Call : Expression
{
    private Expression[] _arguments;
    private State _state;

    public Call(Expression function, Expression[] arguments, State state)
    {
        _left = function;
        _arguments = arguments;
        _state = state;
    }

    public override Value Evaluate()
    {
        Value function = _left.Evaluate();
        if (function.IsCallable())
        {
            return function.Call(_arguments, _state);
        }
        throw new RuntimeException($"Expected builtin or function; cannot call {function.Type}.");
    }
}
