class Token
{
    private double _value;
    private string _lexeme;
    private TokenType _type;
    private int _offset;
    
    public TokenType Type { get { return _type; } }
    public int Offset { get { return _offset; } }

    public Token(double value, string lexeme, int offset)
    {
        _value = value;
        _lexeme = lexeme;
        _type = TokenType.Number;
        _offset = offset;
    }

    public Token(string lexeme, int offset)
    {
        _value = 0;
        _lexeme = lexeme;
        _type = TokenType.Symbol;
        _offset = offset;
    }
    
    public Token(TokenType type, string lexeme, int offset)
    {
        _value = 0;
        _lexeme = lexeme;
        _type = type;
        _offset = offset;
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
    Plus, Minus, Star, Slash, Caret, Query, Colon, Comma, OpenParen, CloseParen,
    
    Bang, Equal, DoubleEqual, BangEqual, Less, Greater, LessEqual, GreaterEqual,

    Number, Symbol,

    And, Or, Until, Let,
    
    Invalid,
}
