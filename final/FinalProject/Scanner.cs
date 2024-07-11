class Scanner
{
    private string _input;
    private int _offset;
    private Dictionary<string,TokenType> _keywordMap;

    public Scanner(string input)
    {
        _input = input;
        _offset = 0;
        _keywordMap = new();
        _keywordMap.Add("and", TokenType.And);
        _keywordMap.Add("or", TokenType.Or);
        _keywordMap.Add("until", TokenType.Until);
        _keywordMap.Add("let", TokenType.Let);
    }

    public Token[] Scan()
    {
        List<Token> tokens = [];

        char? c;
        while ((c = GetChar()) != null)
        {
            switch (c)
            {
                case '+':
                tokens.Add(new Token(TokenType.Plus, "+", _offset - 1));
                break;
                
                case '-':
                tokens.Add(new Token(TokenType.Minus, "-", _offset - 1));
                break;
                
                case '*':
                tokens.Add(new Token(TokenType.Star, "*", _offset - 1));
                break;
                
                case '/':
                tokens.Add(new Token(TokenType.Slash, "/", _offset - 1));
                break;
                
                case '^':
                tokens.Add(new Token(TokenType.Caret, "^", _offset - 1));
                break;
                
                case '(':
                tokens.Add(new Token(TokenType.OpenParen, "(", _offset - 1));
                break;
                
                case ')':
                tokens.Add(new Token(TokenType.CloseParen, ")", _offset - 1));
                break;

                case '?':
                tokens.Add(new Token(TokenType.Query, "?", _offset - 1));
                break;
                
                case ':':
                tokens.Add(new Token(TokenType.Colon, ":", _offset - 1));
                break;

                case ',':
                tokens.Add(new Token(TokenType.Comma, ",", _offset - 1));
                break;

                case '!':
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.BangEqual, "!=", _offset - 1));
                    _offset += 1;
                }
                else
                {
                    tokens.Add(new Token(TokenType.Bang, "!", _offset - 1));
                }
                break;

                case '=':
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.DoubleEqual, "==", _offset - 1));
                    _offset += 1;
                }
                else
                {
                    tokens.Add(new Token(TokenType.Equal, "=", _offset - 1));
                }
                break;
                
                case '<':
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.LessEqual, "<=", _offset - 1));
                    _offset += 1;
                }
                else
                {
                    tokens.Add(new Token(TokenType.Less, "<", _offset - 1));
                }
                break;
                
                case '>':
                if (Peek() == '=')
                {
                    tokens.Add(new Token(TokenType.GreaterEqual, ">=", _offset - 1));
                    _offset += 1;
                }
                else
                {
                    tokens.Add(new Token(TokenType.Greater, ">", _offset - 1));
                }
                break;
                
                default:
                if (char.IsNumber((char)c) || c == '.')
                {
                    int offset = _offset;
                    _offset -= 1;
                    string lexeme;
                    double value = ConsumeNumber(out lexeme);

                    tokens.Add(new Token(value, lexeme, offset));
                }
                else if (IsAlpha((char)c))
                {
                    int offset = _offset;
                    _offset -= 1;
                    string lexeme = ConsumeSymbol();

                    TokenType type;
                    if (_keywordMap.TryGetValue(lexeme, out type))
                    {
                        tokens.Add(new Token(type, lexeme, offset));
                    }
                    else
                    {
                        tokens.Add(new Token(lexeme, offset));
                    }
                }
                else if (!char.IsWhiteSpace((char)c))
                {
                    // TODO: store error info in Token? then return multiple errors?
                    // In any case just returning null doesn't feel quite right
                    _offset -= 1;
                    int startOffset = _offset;
                    string lexeme = ConsumeInvalid();

                    Error(startOffset);
                    return null;
                }
                break;
            }
        }

        return tokens.ToArray();
    }

    private char? Peek()
    {
        if (_offset >= _input.Length)
        {
            return null;
        }

        return _input[_offset];
    }

    private char? GetChar()
    {
        if (_offset >= _input.Length)
        {
            return null;
        }

        return _input[_offset++];
    }

    private double ConsumeNumber(out string lexeme)
    {
        lexeme = "";
        
        char? c;
        while ((c = GetChar()) != null && (char.IsNumber((char)c) || c == '.'))
        {
            lexeme += c;
        }

        if (c != null)
        {
            _offset -= 1;
        }
        double result = 0;
        double.TryParse(lexeme, out result);

        return result;
    }

    private bool IsAlpha(char c)
    {
        bool result = 'A' <= c && c <= 'Z';
        result = result || ('a' <= c && c <= 'z');
        result = result || c == '_';
        return result;
    }

    private string ConsumeSymbol()
    {
        string lexeme = "";
        
        char? c;
        while ((c = GetChar()) != null && IsAlpha((char)c))
        {
            lexeme += c;
        }

        if (c != null)
        {
            _offset -= 1;
        }

        return lexeme;
    }
    
    private string ConsumeInvalid()
    {
        string lexeme = "";
        
        char? c;
        while ((c = GetChar()) != null && !char.IsWhiteSpace((char)c))
        {
            lexeme += c;
        }

        if (c != null)
        {
            _offset -= 1;
        }

        return lexeme;
    }

    private void Error(int startOffset)
    {
        Console.Write("  ");
        for (int i = 0; i < startOffset; ++i) {
            Console.Write(" ");
        }
        for (int i = startOffset; i < _offset; ++i) {
            Console.Write("^");
        }

        Console.WriteLine();
        Console.WriteLine("Syntax Error: Unrecognized symbol.");
    }
}
