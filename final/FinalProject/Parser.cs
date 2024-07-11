class Parser
{
    private Token[] _tokens;
    private int _current;
    private int _last;
    private Expression _root;
    private State _state;
    // Idea for errors in parsing
    // Keep a list of erroneous Tokens; maybe paired with an error enum
    // Then report this info with something like a GetError() method
    // Maybe even just keep one error?
    private List<string> _errorMsg;

    public Parser(Token[] tokens, State state)
    {
        _tokens = tokens;
        _current = 0;
        _last = tokens.Length;
        _root = null;
        _state = state;
        _errorMsg = new();
    }

    public bool Parse(out Value result)
    {
        _root = MatchExpression();
        Token error;
        while ((error = GetToken()) != null)
        {
            _errorMsg.Add($"Unexpected token '{error.GetLexeme()}' at {error.Offset}.");
        }

        if (_errorMsg.Count == 0)
        {
            result = _root.Evaluate();
            return true;
        }
        
        foreach (string error_msg in _errorMsg)
        {
            Console.WriteLine(error_msg);
        }

        result = new Value();
        return false;
    }

    private Expression MatchExpression()
    {
        return MatchSeries();
    }

    private Expression MatchSeries()
    {
        Expression root = MatchRepeat();

        while (true)
        {
            if (Match(TokenType.Comma))
            {
                root = new Series(root, MatchRepeat());
            }
            else
            {
                return root;
            }
        }
    }

    private Expression MatchRepeat()
    {
        Expression root = MatchAssignment();

        while (true)
        {
            if (Match(TokenType.Until))
            {
                root = new Repeat(root, MatchAssignment());
            }
            else
            {
                return root;
            }
        }
    }

    private Expression MatchAssignment()
    {
        if (Match(TokenType.Let))
        {
            Token symbol = GetToken();
            if (symbol.Type != TokenType.Symbol)
            {
                _errorMsg.Add($"Expected symbol at {symbol.Offset}; got '{symbol.GetLexeme()}' instead.");
                return new Literal(0);
            }
            if (!Match(TokenType.Equal))
            {
                _errorMsg.Add($"Expected '=' at {symbol.Offset}; got '{symbol.GetLexeme()}' instead.");
                return new Literal(0);
            }

            return new Assignment(symbol.GetLexeme(), MatchAssignment(), _state);
        }

        return MatchTernary();
    }

    private Expression MatchTernary()
    {
        Expression condition = MatchLogical();

        if (Match(TokenType.Query))
        {
            Expression left = MatchLogical();

            if (!Match(TokenType.Colon))
            {
                Token error = GetToken();
                _errorMsg.Add($"Expected ':' at {error.Offset}; found '{error.GetLexeme()}' instead.");
                return new Literal(0);
            }

            // Important: this should be MatchTernary() so it can recurse
            Expression right = MatchTernary();

            return new Ternary(condition, left, right);
        }

        return condition;
    }

    private Expression MatchLogical()
    {
        Expression root = MatchComparison();

        while (true)
        {
            if (Match(TokenType.And))
            {
                root = new LogicalAnd(root, MatchComparison());
            }
            else if (Match(TokenType.Or))
            {
                root = new LogicalOr(root, MatchComparison());
            }
            else
            {
                return root;
            }
        }
    }

    private Expression MatchComparison()
    {
        Expression root = MatchTerm();

        while (true)
        {
            if (Match(TokenType.Less))
            {
                root = new LessThan(root, MatchTerm());
            }
            else if (Match(TokenType.LessEqual))
            {
                root = new LessOrEqualTo(root, MatchTerm());
            }
            else if (Match(TokenType.Greater))
            {
                root = new GreaterThan(root, MatchTerm());
            }
            else if (Match(TokenType.GreaterEqual))
            {
                root = new GreaterOrEqualTo(root, MatchTerm());
            }
            else if (Match(TokenType.DoubleEqual))
            {
                root = new EqualTo(root, MatchTerm());
            }
            else if (Match(TokenType.BangEqual))
            {
                root = new LogicalNot(new EqualTo(root, MatchTerm()));
            }
            else
            {
                return root;
            }
        }
    }

    private Expression MatchTerm()
    {
        Expression root = MatchFactor();

        while (true)
        {
            if (Match(TokenType.Plus))
            {
                root = new Addition(root, MatchFactor());
            }
            else if (Match(TokenType.Minus))
            {
                root = new Subtraction(root, MatchFactor());
            }
            else
            {
                return root;
            }
        }
    }

    private Expression MatchFactor()
    {
        Expression root = MatchPower();

        while (true)
        {
            if (Match(TokenType.Star))
            {
                root = new Multiplication(root, MatchPower());
            }
            else if (Match(TokenType.Slash))
            {
                root = new Division(root, MatchPower());
            }
            else
            {
                return root;
            }
        }
    }

    private Expression MatchPower()
    {
        Expression lhs = MatchUnary();

        if (Match(TokenType.Caret))
        {
            return new Power(lhs, MatchPower());
        }

        return lhs;
    }

    private Expression MatchUnary()
    {
        if (Match(TokenType.Minus))
        {
            return new Negation(MatchUnary());
        }
        else if (Match(TokenType.Bang))
        {
            return new LogicalNot(MatchUnary());
        }
        else
        {
            return MatchLiteral();
        }
    }

    private Expression MatchLiteral()
    {
        if (Peek()?.Type == TokenType.Number)
        {
            return new Literal((double)GetToken().GetValue());
        }
        else if (Peek()?.Type == TokenType.Symbol)
        {
            return new Symbol(GetToken().GetLexeme(), _state);
        }
        else if (Match(TokenType.OpenParen))
        {
            int openOffset = _tokens[_current-1].Offset;
            Expression expr = MatchExpression();
            if (!Match(TokenType.CloseParen))
            {
                _errorMsg.Add($"Unmatched '(' at {openOffset}.");
                return new Literal(0);
            }
            
            return expr;
        }
        else {
            // Token could be null if we reached the end of input
            Token error = GetToken();
            if (error == null)
            {
                _errorMsg.Add("Unexpected EOF.");
            }
            else
            {
                _errorMsg.Add($"Unexpected token '{error.GetLexeme()}' at {error.Offset}.");
            }
            return new Literal(0);
        }
    }

    private bool Match(TokenType type)
    {
        if (Peek()?.Type != type)
        {
            return false;
        }
        
        _current += 1;
        return true;
    }

    // private int SeekToken(TokenType type)
    // {
    //     return SeekToken(TokenType.Invalid, type);
    // }

    // private int SeekToken(TokenType left, TokenType right)
    // {
    //     int depth = 1;
    //     int index = _current;

    //     while (index < _last && depth != 0)
    //     {
    //         if (left != TokenType.Invalid && _tokens[index].Type == left)
    //         {
    //             depth += 1;
    //         }
    //         else if (_tokens[index].Type == right)
    //         {
    //             depth -= 1;
    //         }
    //         index += 1;
    //     }

    //     if (index >= _last && (depth != 0 || _tokens[index-1].Type != right))
    //     {
    //         return -1;
    //     }

    //     return index-1;
    // }

    private Token Peek()
    {
        if (_current >= _last)
        {
            return null;
        }
        
        return _tokens[_current];
    }

    private Token GetToken()
    {
        if (_current >= _last)
        {
            return null;
        }
        
        return _tokens[_current++];
    }
}
