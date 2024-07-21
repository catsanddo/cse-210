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
        return ParseSeries();
    }

    public Expression ParseSeries()
    {
        Expression left = ParseAssignment();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.Comma))
            {
                left = new Comma(left, ParseAssignment());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParseAssignment()
    {
        Expression left = ParseLoop();

        if (_scanner.MatchToken(TokenType.Equal))
        {
            return new Assignment(left, ParseAssignment());
        }

        return left;
    }

    public Expression ParseLoop()
    {
        Expression left = ParseTernary();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.Until))
            {
                left = new Until(left, ParseTernary());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParseTernary()
    {
        Expression condition = ParseLogical();

        if (_scanner.MatchToken(TokenType.Query))
        {
            Expression left = ParseLogical();

            if (!_scanner.MatchToken(TokenType.Colon))
            {
                Token error = _scanner.GetToken();
                _errors.Add($"Expected ':' at {error.Offset}; got {error.GetLexeme()} instead.");
                return new Literal(new Value());
            }

            return new Ternary(condition, left, ParseTernary());
        }

        return condition;
    }

    public Expression ParseLogical()
    {
        Expression left = ParseComparison();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.And))
            {
                left = new LogicalAnd(left, ParseComparison());
            }
            else if (_scanner.MatchToken(TokenType.Or))
            {
                left = new LogicalOr(left, ParseComparison());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParseComparison()
    {
        Expression left = ParseTerm();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.DoubleEqual))
            {
                left = new EqualTo(left, ParseTerm());
            }
            else if (_scanner.MatchToken(TokenType.BangEqual))
            {
                left = new LogicalNot(new EqualTo(left, ParseTerm()));
            }
            else if (_scanner.MatchToken(TokenType.Less))
            {
                left = new LessThan(left, ParseTerm());
            }
            else if (_scanner.MatchToken(TokenType.LessEqual))
            {
                left = new LessOrEqualTo(left, ParseTerm());
            }
            else if (_scanner.MatchToken(TokenType.Greater))
            {
                left = new LessThan(ParseTerm(), left);
            }
            else if (_scanner.MatchToken(TokenType.GreaterEqual))
            {
                left = new LessOrEqualTo(ParseTerm(), left);
            }
            else
            {
                return left;
            }
        }
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
        Expression left = ParsePower();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.Star))
            {
                left = new Multiplication(left, ParsePower());
            }
            else if (_scanner.MatchToken(TokenType.Slash))
            {
                left = new Division(left, ParsePower());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParsePower()
    {
        Expression left = ParseAppend();

        if (_scanner.MatchToken(TokenType.Caret))
        {
            return new Power(left, ParsePower());
        }

        return left;
    }

    public Expression ParseAppend()
    {
        Expression left = ParseUnary();

        while (true)
        {
            if (_scanner.MatchToken(TokenType.LessLess))
            {
                left = new Append(left, ParseUnary());
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParseUnary()
    {
        if (_scanner.MatchToken(TokenType.Minus))
        {
            return new Negation(ParseUnary());
        }
        else if (_scanner.MatchToken(TokenType.Bang))
        {
            return new LogicalNot(ParseUnary());
        }
        return ParseSuffix();
    }

    public Expression ParseSuffix()
    {
        Expression left = ParsePrimary();

        while (true)
        {
            if (_scanner.PeekToken()?.Type == TokenType.LeftBracket)
            {
                Token open = _scanner.GetToken();
                Expression index = ParseAssignment();
                if (!_scanner.MatchToken(TokenType.RightBracket))
                {
                    _errors.Add($"Unmatched '[' at {open.Offset}.");
                    return new Literal(new Value());
                }
                return new Index(left, index);
            }
            else if (_scanner.PeekToken()?.Type == TokenType.LeftParen)
            {
                Token open = _scanner.GetToken();
                List<Expression> arguments = new();
                
                if (_scanner.PeekToken()?.Type != TokenType.RightParen)
                {
                    arguments.Add(ParseAssignment());
                }
                while (_scanner.MatchToken(TokenType.Comma))
                {
                    arguments.Add(ParseAssignment());
                }
                if (!_scanner.MatchToken(TokenType.RightParen))
                {
                    _errors.Add($"Unmatched '(' at {open.Offset}.");
                    return new Literal(new Value());
                }
                return new Call(left, arguments.ToArray(), _state);
            }
            else
            {
                return left;
            }
        }
    }

    public Expression ParsePrimary()
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
        else if (_scanner.PeekToken()?.Type == TokenType.LeftBrace)
        {
            Token open = _scanner.GetToken();
            List<string> parameters = new();
            while (!_scanner.MatchToken(TokenType.RightBrace))
            {
                Token parameter = _scanner.GetToken();
                if (parameter.Type != TokenType.Identifier)
                {
                    _errors.Add($"Expected identifier at {parameter.Offset}; got {parameter.Type} instead.");
                    return new Literal(new Value());
                }
                parameters.Add(parameter.GetLexeme());
            }
            Expression body = ParseAssignment();
            
            return new Literal(new Value(parameters.ToArray(), body));
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
