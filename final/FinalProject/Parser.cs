class Parser
{
    private Scanner _scanner;
    private List<string> _errors;
    private State _state;

    public Parser(StreamReader source)
    {
        if (source == null)
        {
            _scanner = null;
        }
        else
        {
            _scanner = new(source);
        }
        _state = new();
        _errors = new();
    }

    public void FeedLine(string source)
    {
        _scanner = new(source);
    }

    public Value Parse()
    {
        Expression expr = ParseExpression();
        if (_errors.Count != 0)
        {
            foreach (string error in _errors)
            {
                Console.WriteLine(error);
            }
            _errors.Clear();
            return null;
        }

        if (_scanner.PeekToken() != null)
        {
            Token extra;
            while ((extra = _scanner.GetToken()) != null)
            {
                Console.WriteLine($"Unexpected token '{extra.GetLexeme()}' at {extra.Offset}.");
            }
            return null;
        }

        try {
            return expr.Evaluate();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Runtime error: {e}");
        }

        return null;
    }

    public Expression ParseExpression()
    {
        return ParseTerm();
    }

    public Expression ParseTerm()
    {
        Expression left = ParseFactor();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.Plus))
            {
                left = new Addition(left, ParseFactor());
            }
            else if (_scanner.MatchToken(TokenType.Minus))
            {
                left = new Subtraction(left, ParseFactor());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParseFactor()
    {
        Expression left = ParseLiteral();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.Star))
            {
                left = new Multiplication(left, ParseFactor());
            }
            else if (_scanner.MatchToken(TokenType.Slash))
            {
                left = new Division(left, ParseFactor());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParseLiteral()
    {
        if (_scanner.PeekToken()?.Type == TokenType.Number)
        {
            Token number = _scanner.GetToken();
            return new Literal(new Value((double)number.GetNumber()));
        }
        else if (_scanner.PeekToken()?.Type == TokenType.Array)
        {
            Token array = _scanner.GetToken();
            return new Literal(new Value(array.GetArray()));
        }
        else if (_scanner.MatchToken(TokenType.Nil))
        {
            return new Literal(new Value());
        }
        else if (_scanner.PeekToken()?.Type == TokenType.Identifier)
        {
            return new Identifier(_scanner.GetToken().GetLexeme(), _state);
        }
        else if (_scanner.PeekToken()?.Type == TokenType.LeftParen)
        {
            Token paren = _scanner.GetToken();
            Expression expr = ParseExpression();
            if (!_scanner.MatchToken(TokenType.RightParen))
            {
                _errors.Add($"Unmatched '(' at {paren.Offset}.");
                return new Literal(new Value());
            }
            return expr;
        }
        // TODO: function literals
        
        if (_scanner.PeekToken()?.Type == TokenType.Invalid)
        {
            Token invalid = _scanner.GetToken();
            _errors.Add($"Invalid token '{invalid.GetLexeme()} at {invalid.Offset}'.");
            return new Literal(new Value());
        }
        else if (_scanner.PeekToken() == null)
        {
            _errors.Add("Unexpected EOF.");
            return new Literal(new Value());
        }

        Token unexpected = _scanner.GetToken();
        _errors.Add($"Unexpected token '{unexpected.GetLexeme()}' at {unexpected.Offset}.");
        return new Literal(new Value());
    }
}
