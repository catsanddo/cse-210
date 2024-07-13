class Token
{
    private double _number;
    private double[] _array;
    private int _startOffset;
    private int _endOffset;
    private string _lexeme;
    private TokenType _type;

    public TokenType Type
    {
        get { return _type; }
    }

    public int Offset
    {
        get { return _startOffset; }
    }
    
    public Token(TokenType type, string lexeme, int offset)
    {
        _type = type;
        _lexeme = lexeme;
        _startOffset = offset;
        _endOffset = offset + lexeme.Length;
    }

    public Token(double number, string lexeme, int offset)
    {
        _type = TokenType.Number;
        _number = number;
        _lexeme = lexeme;
        _startOffset = offset;
        _endOffset = offset + lexeme.Length;
    }

    public Token(double[] array, string lexeme, int offset)
    {
        _type = TokenType.Array;
        _array = array;
        _lexeme = lexeme;
        _startOffset = offset;
        _endOffset = offset + lexeme.Length;
    }

    public double? GetNumber()
    {
        if (_type != TokenType.Number)
        {
            return null;
        }
        return _number;
    }

    public double[] GetArray()
    {
        if (_type != TokenType.Array)
        {
            return null;
        }
        return _array;
    }

    public string GetLexeme()
    {
        return _lexeme;
    }
}

enum TokenType
{
    Comma, Plus, Minus, Star, Slash, Caret, Query, Colon,
    LeftBracket, RightBracket, LeftParen, RightParen, LeftBrace, RightBrace,

    Less, Greater, LessEqual, GreaterEqual, LessLess,
    Equal, DoubleEqual, Bang, BangEqual,

    Until, Nil, And, Or,

    Identifier, Number, Array,

    Invalid,
}
