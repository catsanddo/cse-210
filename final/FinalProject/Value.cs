class Value
{
    private double? _number;
    private string _symbol;
    private double[] _array;
    private string[] _params;
    private Expression _body;
    private ValueType _type;

    public ValueType Type {
        get { return _type; }
    }

    public Value(double number)
    {
        _type = ValueType.Number;
        _number = number;
    }

    public Value(double[] array)
    {
        _type = ValueType.Array;
        _array = array;
    }

    public Value(string symbol)
    {
        _type = ValueType.Symbol;
        _symbol = symbol;
    }

    public Value(string[] p, Expression body)
    {
        _type = ValueType.Function;
        _params = p;
        _body = body;
    }

    public Value()
    {
        _type = ValueType.Nil;
    }

    public double? GetNumber()
    {
        if (_type != ValueType.Number)
        {
            return null;
        }
        return _number;
    }

    public string GetSymbol()
    {
        if (_type != ValueType.Symbol)
        {
            return null;
        }
        return _symbol;
    }

    public double[] GetArray()
    {
        if (_type != ValueType.Array)
        {
            return null;
        }
        return _array;
    }

    public Value Call(Expression[] p, State state)
    {
        state.PushFrame();
        for (int i = 0; i < _params.Length; ++i)
        {
            // TODO: figure out what to do with smaller params list
            // options: error, replace with nil in local scope (make sure they don't leak to outer scopes)
            state.SetValue(_params[i], p[i].Evaluate());
        }

        return _body.Evaluate();
    }

    public override string ToString()
    {
        switch (_type)
        {
            case ValueType.Array:
            return $"{_array}";
            
            case ValueType.Number:
            return $"{_number}";
            
            case ValueType.Symbol:
            return $"<{_symbol}>";
            
            case ValueType.Function:
            return $"<function>";
            
            case ValueType.Nil:
            return $"nil";
        }
        return "undefined";
    }
}

enum ValueType
{
    Array,
    Number,
    Symbol,
    Function,
    Nil,
}
