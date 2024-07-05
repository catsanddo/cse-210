class Scanner
{
    private string _input;
    private int _offset;

    public Scanner(string input)
    {
        _input = input;
        _offset = 0;
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
                tokens.Add(new Token(TokenType.Plus, "+"));
                break;
                
                case '-':
                tokens.Add(new Token(TokenType.Minus, "-"));
                break;
                
                case '*':
                tokens.Add(new Token(TokenType.Star, "*"));
                break;
                
                case '/':
                tokens.Add(new Token(TokenType.Slash, "/"));
                break;
                
                case '^':
                tokens.Add(new Token(TokenType.Caret, "^"));
                break;
                
                case '(':
                tokens.Add(new Token(TokenType.OpenParen, "("));
                break;
                
                case ')':
                tokens.Add(new Token(TokenType.CloseParen, ")"));
                break;

                case '?':
                tokens.Add(new Token(TokenType.Query, "?"));
                break;
                
                case ':':
                tokens.Add(new Token(TokenType.Colon, ":"));
                break;
                
                default:
                if (char.IsNumber((char)c) || c == '.')
                {
                    _offset -= 1;
                    string lexeme;
                    double value = ConsumeNumber(out lexeme);

                    tokens.Add(new Token(value, lexeme));
                }
                else if (!char.IsWhiteSpace((char)c))
                {
                    // TODO: store error info in Token? then return multiple errors?
                    // In any case just returning null doesn't feel quite right
                    _offset -= 1;
                    int startOffset = _offset;
                    string lexeme = ConsumeSymbol();

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

    private string ConsumeSymbol()
    {
        string lexeme = "";
        
        char? c;
        // NOTE: symbols only break on whitespace; <foobar+4> is a valid symbol
        // Not the biggest deal but undesireable especially if we support assigning symbols
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
