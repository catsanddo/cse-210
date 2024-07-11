using System.IO;

class Scanner
{
    private string _source;
    private int _localOffset;
    private int _offset;
    private StreamReader _reader;
    private Dictionary<string,TokenType> _keywords;
    private Token _buffer;

    public Scanner(string source)
    {
        _source = source;
        _localOffset = 0;
        _offset = 0;
        _reader = null;
        _keywords = new();
        _keywords.Add("until", TokenType.Until);
        _keywords.Add("nil", TokenType.Nil);
        _keywords.Add("and", TokenType.And);
        _keywords.Add("or", TokenType.Or);
        _buffer = null;
    }

    public Scanner(StreamReader source)
    {
        _source = "";
        _localOffset = 0;
        _offset = 0;
        _reader = source;
        _keywords = new();
        _keywords.Add("until", TokenType.Until);
        _keywords.Add("nil", TokenType.Nil);
        _keywords.Add("and", TokenType.And);
        _keywords.Add("or", TokenType.Or);
        _buffer = null;
    }

    ~Scanner()
    {
        if (_reader != null)
        {
            _reader.Dispose();
        }
    }

    public Token GetToken()
    {
        if (_buffer == null)
        {
            return Tokenize();
        }
        Token result = _buffer;
        _buffer = null;
        return result;
    }

    public Token PeekToken()
    {
        if (_buffer == null)
        {
            _buffer = Tokenize();
        }
        return _buffer;
    }

    public bool MatchToken(TokenType type)
    {
        if (_buffer == null)
        {
            _buffer = Tokenize();
        }
        if (_buffer?.Type == type)
        {
            _buffer = null;
            return true;
        }
        return false;
    }

    private Token Tokenize()
    {
        // Skip any leading whitespace
        char? ws;
        while ((ws = PeekChar()) != null && char.IsWhiteSpace((char)ws))
        {
            GetChar();
        }

        // If we ran out of characters, we ran out of tokens
        if (PeekChar() == null)
        {
            return null;
        }

        char c = (char)GetChar();
        switch (c)
        {
            case ',':
            return new Token(TokenType.Comma, ",", _offset-1);

            case '+':
            return new Token(TokenType.Plus, "+", _offset-1);

            case '-':
            return new Token(TokenType.Minus, "-", _offset-1);

            case '*':
            return new Token(TokenType.Star, "*", _offset-1);

            case '/':
            return new Token(TokenType.Slash, "/", _offset-1);

            case '^':
            return new Token(TokenType.Caret, "^", _offset-1);

            case '?':
            return new Token(TokenType.Query, "?", _offset-1);

            case ':':
            return new Token(TokenType.Colon, ":", _offset-1);

            case '[':
            return new Token(TokenType.LeftBracket, "[", _offset-1);

            case ']':
            return new Token(TokenType.RightBracket, "]", _offset-1);

            case '(':
            return new Token(TokenType.LeftParen, "(", _offset-1);

            case ')':
            return new Token(TokenType.RightParen, ")", _offset-1);

            case '{':
            return new Token(TokenType.LeftBrace, "{", _offset-1);

            case '}':
            return new Token(TokenType.RightBrace, "}", _offset-1);

            case '<':
            if (MatchChar('='))
            {
                return new Token(TokenType.LessEqual, "<=", _offset-2);
            }
            else if (MatchChar('<'))
            {
                return new Token(TokenType.LessLess, "<<", _offset-2);
            }
            return new Token(TokenType.Less, "<", _offset-1);
            
            case '>':
            if (MatchChar('='))
            {
                return new Token(TokenType.GreaterEqual, ">=", _offset-2);
            }
            return new Token(TokenType.Greater, ">", _offset-1);
            
            case '=':
            if (MatchChar('='))
            {
                return new Token(TokenType.DoubleEqual, "==", _offset-2);
            }
            return new Token(TokenType.Equal, "=", _offset-1);
            
            case '!':
            if (MatchChar('='))
            {
                return new Token(TokenType.BangEqual, "!=", _offset-2);
            }
            return new Token(TokenType.Bang, "!", _offset-1);

            default:
            if (IsDigit(c) || c == '.')
            {
                // Parse number literal
                UngetChar(c);
                int offset = _offset;
                string lexeme;
                double number = ConsumeNumber(out lexeme);
                return new Token(number, lexeme, offset);
            }
            else if (c == '"')
            {
                // Parse ASCII string literal as array of numbers
                UngetChar(c);
                int offset = _offset;
                string lexeme;
                double[] array = ConsumeArray(out lexeme);
                return new Token(array, lexeme, offset);
            }
            else if (IsAlpha(c))
            {
                // Parse valid identifiers or keywords
                UngetChar(c);
                int offset = _offset;
                string word = ConsumeIdentifier();
                TokenType type;
                if (!_keywords.TryGetValue(word, out type))
                {
                    type = TokenType.Identifier;
                }
                return new Token(type, word, offset);
            }
            else
            {
                // Must be an invalid character
                // Swallow the offending characters into one token
                UngetChar(c);
                int offset = _offset;
                string word = ConsumeInvalid();
                return new Token(TokenType.Invalid, word, offset);
            }
        }
    }

    private bool IsDigit(char c)
    {
        return ('0' <= c && c <= '9');
    }

    private bool IsAlpha(char c)
    {
        return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || c == '_';
    }

    private bool IsHex(char c)
    {
        return IsDigit(c) || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f');
    }

    private bool IsValid(char c)
    {
        return IsAlpha(c) || IsDigit(c) || c == '.' || c == ',' || c == '+' ||
            c == '-' || c == '*' || c == '/' || c == '^' || c == '?' ||
            c == ':' || c == '[' || c == ']' || c == '(' || c == ')' ||
            c == '{' || c == '}' || c == '<' || c == '>' || c == '=' ||
            c == '!' || c == '"';
    }

    private double ConsumeNumber(out string lexeme)
    {
        lexeme = "";
        char? c;
        while ((c = PeekChar()) != null && IsDigit((char)c))
        {
            GetChar();
            lexeme += (char)c;
        }

        if (PeekChar() == '.')
        {
            GetChar();
            lexeme += '.';
        }

        while ((c = PeekChar()) != null && IsDigit((char)c))
        {
            GetChar();
            lexeme += (char)c;
        }

        double result;
        double.TryParse(lexeme, out result);
        return result;
    }

    // TODO: parse that string!
    private double[] ConsumeArray(out string lexeme)
    {
        lexeme = "";
        return null;
    }

    private string ConsumeIdentifier()
    {
        string result = "";
        char? c;
        while ((c = PeekChar()) != null && (IsAlpha((char)c) || IsDigit((char)c)))
        {
            GetChar();
            result += (char)c;
        }

        return result;
    }

    private string ConsumeInvalid()
    {
        string result = "";
        char? c;
        while ((c = PeekChar()) != null && !IsValid((char)c))
        {
            GetChar();
            result += (char)c;
        }

        return result;
    }

    private char? GetChar()
    {
        if (_reader != null && _localOffset >= _source.Length)
        {
            _localOffset = 0;
            _source = _reader.ReadLine();
        }
        if (_localOffset < _source.Length)
        {
            _offset += 1;
            return _source[_localOffset++];
        }

        return null;
    }

    private char? PeekChar()
    {
        if (_reader != null && _localOffset >= _source.Length)
        {
            _localOffset = 0;
            _source = _reader.ReadLine();
        }
        if (_localOffset < _source.Length)
        {
            return _source[_localOffset];
        }

        return null;
    }

    private void UngetChar(char c)
    {
        _offset -= 1;
        if (_source.Length == 0)
        {
            _source += c;
            return;
        }
        _localOffset -= 1;
    }

    private bool MatchChar(char c)
    {
        if (PeekChar() == c)
        {
            GetChar();
            return true;
        }
        return false;
    }
}
