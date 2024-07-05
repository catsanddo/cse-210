class Token
{
    private double _value;
    private string _lexeme;
    private TokenType _type;
    public TokenType Type { get { return _type; } }

    public Token(double value, string lexeme)
    {
        _value = value;
        _lexeme = lexeme;
        _type = TokenType.Number;
    }

    public Token(TokenType type, string lexeme)
    {
        _value = 0;
        _lexeme = lexeme;
        _type = type;
    }

    public Token(string symbol)
    {
        _value = 0;
        _lexeme = symbol;
        _type = TokenType.Symbol;
    }

    public string GetLexeme()
    {
        return _lexeme;
    }

    public double? GetValue()
    {
        if (_type == TokenType.Number)
        {
            return _value;
        }

        return null;
    }

    public override string ToString()
    {
        if (_type == TokenType.Number)
        {
            return $"{_value}";
        }
        else if (_type == TokenType.Symbol)
        {
            return $"<{_lexeme}>";
        }

        return $"{_type}";
    }
}

enum TokenType
{
    Number,
    Symbol,
    Plus,
    Minus,
    Star,
    Slash,
    Caret,
    OpenParen,
    CloseParen,
    Query,
    Colon,
    Invalid,
}
