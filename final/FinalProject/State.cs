class State
{
    private Dictionary<string,Value> _global;
    private List<Dictionary<string,Value>> _localFrames;

    public State()
    {
        _global = new();
        _localFrames = new();
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
        if (_localFrames.Count == 0)
        {
            _global[name] = value;
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
