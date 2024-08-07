class Value
{
    private ValueType _type;
    private double[] _array;
    private double _number;
    private string[] _paramList;
    private Expression _body;
    private Builtin.Id _builtinId;

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
    
    public Value(Builtin.Id body)
    {
        _type = ValueType.Builtin;
        _builtinId = body;
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
        return _type == ValueType.Function || _type == ValueType.Builtin;
    }

    public Value Call(Expression[] args, State state)
    {
        Value[] evaluatedArgs = new Value[args.Length];

        for (int i = 0; i < args.Length; ++i)
        {
            evaluatedArgs[i] = args[i].Evaluate();
        }
        
        if (_type == ValueType.Builtin)
        {
            return Builtin.Execute(_builtinId, evaluatedArgs);
        }
        
        state.PushFrame();
        if (args.Length != _paramList.Length)
        {
            throw new RuntimeException($"Expected {_paramList.Length} arguments; got {args.Length}.");
        }
        for (int i = 0; i < _paramList.Length; ++i)
        {
            state.SetValue(_paramList[i], evaluatedArgs[i]);
        }
        Value result = _body.Evaluate();
        state.PopFrame();
        
        return result;
    }

    public bool Equals(Value other)
    {
        if (_type != other._type)
        {
            return false;
        }
        switch (_type)
        {
            case ValueType.Nil:
            return true;

            case ValueType.Number:
            return _number == other._number;

            case ValueType.Array:
            if (_array.Length != other._array.Length)
            {
                return false;
            }
            for (int i = 0; i < _array.Length; ++i)
            {
                if (_array[i] != other._array[i])
                {
                    return false;
                }
            }
            return true;

            case ValueType.Function:
            return _body == other._body;

            case ValueType.Builtin:
            return _builtinId == other._builtinId;
        }

        return false;
    }

    public bool LessThan(Value other)
    {
        if (_type != other._type)
        {
            return false;
        }
        switch (_type)
        {
            case ValueType.Nil:
            return false;

            case ValueType.Number:
            return _number < other._number;

            case ValueType.Array:
            int length = Math.Min(_array.Length, other._array.Length);
            for (int i = 0; i < length; ++i)
            {
                if (_array[i] > other._array[i])
                {
                    return false;
                }
            }
            if (_array.Length > other._array.Length)
            {
                return false;
            }
            return !Equals(other);

            case ValueType.Function:
            return false;

            case ValueType.Builtin:
            return false;
        }

        return false;
    }

    public override string ToString()
    {
        switch (_type)
        {
            case ValueType.Array:
            if (_array.Length == 0)
            {
                return "{}";
            }
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
            return $"<builtin:{_builtinId}>";

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
