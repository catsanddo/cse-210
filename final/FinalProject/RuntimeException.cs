class RuntimeException : Exception
{
    private string _error;
    
    public RuntimeException(string error) : base()
    {
        _error = error;
    }

    public override string ToString()
    {
        return _error;
    }
}
