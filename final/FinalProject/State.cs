class State
{
    private Dictionary<string,Value> _global;
    private List<Dictionary<string,Value>> _localFrames;

    public State()
    {
        _global = new();
        _localFrames = new();

        _global.Add("sqrt", new Value(Builtin.Id.Sqrt));
        _global.Add("abs", new Value(Builtin.Id.Abs));
        _global.Add("len", new Value(Builtin.Id.Len));
        _global.Add("dim", new Value(Builtin.Id.Dim));
        _global.Add("sin", new Value(Builtin.Id.Sin));
        _global.Add("cos", new Value(Builtin.Id.Cos));
        _global.Add("tan", new Value(Builtin.Id.Tan));
        _global.Add("asin", new Value(Builtin.Id.Asin));
        _global.Add("acos", new Value(Builtin.Id.Acos));
        _global.Add("atan", new Value(Builtin.Id.Atan));
        _global.Add("input", new Value(Builtin.Id.Input));
        _global.Add("output", new Value(Builtin.Id.Output));
        _global.Add("quit", new Value(Builtin.Id.Quit));
        _global.Add("pi", new Value(Math.PI));
    }
    
    public void PushFrame()
    {
        _localFrames.Add(new());
    }

    public void PopFrame()
    {
        if (_localFrames.Count == 0)
        {
            return;
        }
        _localFrames.RemoveAt(_localFrames.Count-1);
    }

    public void SetValue(string name, Value value)
    {
        for (int i = _localFrames.Count-1; i >= 0; --i)
        {
            if (_localFrames[i].ContainsKey(name))
            {
                _localFrames[i][name] = value;
                return;
            }
        }
        if (_global.ContainsKey(name))
        {
            _global[name] = value;
            return;
        }
        if (_localFrames.Count == 0)
        {
            _global[name] = value;
            return;
        }
        _localFrames[_localFrames.Count-1][name] = value;
    }

    public Value GetValue(string name)
    {
        Value result;
        for (int i = _localFrames.Count-1; i >= 0; --i)
        {
            if (_localFrames[i].TryGetValue(name, out result))
            {
                return result;
            }
        }
        if (_global.TryGetValue(name, out result))
        {
            return result;
        }
        // TODO: decide on better error handling for undefined values
        // For now return nil
        return new Value();
    }
}
