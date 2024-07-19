static class Builtin
{
    static public Value Execute(Id id, Value[] arguments)
    {
        switch (id)
        {
            case Id.Sqrt:
            return Sqrt(arguments);
            case Id.Abs:
            return Abs(arguments);
            case Id.Len:
            return Len(arguments);
            case Id.Dim:
            return Dim(arguments);
            case Id.Sin:
            return Sin(arguments);
            case Id.Cos:
            return Cos(arguments);
            case Id.Tan:
            return Tan(arguments);
            case Id.Asin:
            return Asin(arguments);
            case Id.Acos:
            return Acos(arguments);
            case Id.Atan:
            return Atan(arguments);
            case Id.Input:
            return Input(arguments);
            case Id.Output:
            return Output(arguments);
            case Id.Quit:
            return Quit(arguments);
            default:
            throw new RuntimeException($"No builtin function with ID {(int)id}");
        }
    }

    static private void AssertArgLength(int expected, Value[] args)
    {
        if (expected != args.Length)
        {
            throw new RuntimeException($"Expected {expected} arguments; got {args.Length}.");
        }
    }

    static private void AssertArgType(Value arg, ValueType type)
    {
        if (arg.Type != type)
        {
            throw new RuntimeException($"Expected argument of type {type}; got {arg.Type}.");
        }
    }

    static private Value Sqrt(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Sqrt((double)arguments[0].GetNumber()));
    }

    static private Value Abs(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Abs((double)arguments[0].GetNumber()));
    }
    
    static private Value Len(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Array);
        int length = arguments[0].GetArray().Length;
        return new Value((double)length);
    }
    
    static private Value Dim(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        int length = (int)arguments[0].GetNumber();
        double[] array = new double[length];
        return new Value(array);
    }

    static private Value Sin(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Sin((double)arguments[0].GetNumber()));
    }

    static private Value Cos(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Cos((double)arguments[0].GetNumber()));
    }

    static private Value Tan(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Tan((double)arguments[0].GetNumber()));
    }

    static private Value Asin(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Asin((double)arguments[0].GetNumber()));
    }

    static private Value Acos(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Acos((double)arguments[0].GetNumber()));
    }

    static private Value Atan(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        AssertArgType(arguments[0], ValueType.Number);
        return new Value(Math.Atan((double)arguments[0].GetNumber()));
    }

    static private Value Input(Value[] arguments)
    {
        int input = Console.Read();
        if (input == -1)
        {
            return new Value((double)0);
        }

        // Translate CRLF to LF ASCII code
        if ((char)input == '\r' && (char)(input = Console.Read()) == '\n')
        {
            return new Value(10);
        }

        return new Value((double)input);
    }

    // TODO: translate LF to CRLF
    static private Value Output(Value[] arguments)
    {
        AssertArgLength(1, arguments);
        if (arguments[0].Type == ValueType.Number)
        {
            Console.Write((char)arguments[0].GetNumber());
            return new Value();
        }
        
        AssertArgType(arguments[0], ValueType.Array);
        string output = "";
        foreach (double ascii in arguments[0].GetArray())
        {
            output += (char)ascii;
        }
        Console.Write(output);
        return new Value();
    }

    static private Value Quit(Value[] arguments)
    {
        AssertArgLength(0, arguments);
        Environment.Exit(0);
        return new Value();
    }

    public enum Id
    {
        Sqrt,
        Abs,
        Len,
        Dim,
        Sin,
        Cos,
        Tan,
        Asin,
        Acos,
        Atan,
        Input,
        Output,
        Quit,
    }
}
