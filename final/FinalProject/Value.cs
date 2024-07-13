class Value
{
    // TODO: store a reference to a builtin database
    private ValueType _type;
    private double[] _array;
    private double _number;
    private string[] _paramList;
    private Expression _body;
    private string _builtinName;

    public ValueType Type
    {
        get { return _type; }
    }

    public Value()
    {
        _type = ValueType.Nil;
    }

    public Value(double[] array)
    {
        _type = ValueType.Array;
        _array = array;
    }

    public Value(double number)
    {
        _type = ValueType.Number;
        _number = number;
    }

    public Value(string[] paramList, Expression body)
    {
        _type = ValueType.Function;
        _paramList = paramList;
        _body = body;
    }
    
    public Value(string[] paramList, string body)
    {
        _type = ValueType.Builtin;
        _paramList = paramList;
        _builtinName = body;
    }

    public double[] GetArray()
    {
        if (_type != ValueType.Array)
        {
            return null;
        }
        return _array;
    }

    public double? GetNumber()
    {
        if (_type != ValueType.Number)
        {
            return null;
        }
        return _number;
    }

    public bool IsTruthy()
    {
        if (_type == ValueType.Number)
        {
            return _number != 0;
        }
        else if (_type == ValueType.Array)
        {
            return _array.Length != 0;
        }
        else if (_type == ValueType.Nil)
        {
            return false;
        }
        return true;
    }

    public bool IsCallable()
    {
        // TODO: check if _builtinName exists as a valid builtin
        return _type == ValueType.Function || _type == ValueType.Builtin;
    }

    public Value Call(Expression[] args, State state)
    {
        // Handle calling non-callable and builtins
        state.PushFrame();
        for (int i = 0; i < _paramList.Length; ++i)
        {
            // TODO: handle mismatch in arg/param list size
            state.SetValue(_paramList[i], args[i].Evaluate());
        }
        Value result = _body.Evaluate();
        state.PopFrame();
        
        return result;
    }

    public override string ToString()
    {
        switch (_type)
        {
            case ValueType.Array:
            string array = "{";
            foreach (double number in _array)
            {
                array += $"{number}, ";
            }
            array = array.Substring(0, array.Length-2) + "}";
            return array;

            case ValueType.Number:
            return $"{_number}";

            case ValueType.Function:
            string parameters = "";
            foreach (string p in _paramList)
            {
                parameters += p + " ";
            }
            return $"<function>{{{parameters.Trim()}}}";

            case ValueType.Builtin:
            return $"<builtin:{_builtinName}>";

            default:
            return "<nil>";
        }
    }
}

enum ValueType
{
    Array,
    Number,
    Function,
    Builtin,
    Nil,
}
