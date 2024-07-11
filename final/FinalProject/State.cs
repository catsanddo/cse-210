class State
{
    private Dictionary<string,Value> _globalTable;
    private List<Dictionary<string,Value>> _localTables;

    public State()
    {
        _globalTable = new();
        _localTables = new();
    }

    public void PushFrame()
    {
        _localTables.Add(new Dictionary<string,Value>());
    }

    public void PopFrame()
    {
        _localTables.RemoveAt(_localTables.Count - 1);
    }

    public Value GetValue(string symbol)
    {
        Value result;

        for (int i = _localTables.Count-1; i >= 0; --i)
        {
            if (_localTables[i].TryGetValue(symbol, out result))
            {
                return result;
            }
        }
        
        if (_globalTable.TryGetValue(symbol, out result))
        {
            return result;
        }
        return new Value();
    }

    public void SetValue(string symbol, Value value)
    {
        if (_localTables.Count == 0)
        {
            _globalTable[symbol] = value;
            return;
        }

        _localTables[_localTables.Count-1][symbol] = value;
    }
}
