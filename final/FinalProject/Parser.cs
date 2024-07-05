class Parser
{
    private Token[] _tokens;
    private int _current;
    private int _last;
    private Expression _root;
    // Idea for errors in parsing
    // Keep a list of erroneous Tokens; maybe paired with an error enum
    // Then report this info with something like a GetError() method
    // Maybe even just keep one error?

    public Parser(Token[] tokens)
    {
        _tokens = tokens;
        _current = 0;
        _last = tokens.Length;
        _root = null;
    }

    public double Parse()
    {
        _root = MatchExpression();
        if (_current < _tokens.Length)
        {
            Console.WriteLine("Unexpected token :c");
            return 0;
        }
        return _root.Evaluate();
    }

    private Expression MatchExpression()
    {
        return MatchTernary();
    }

    private Expression MatchTernary()
    {
        Expression condition = MatchTerm();

        if (Match(TokenType.Query))
        {
            Expression left = MatchTerm();

            if (!Match(TokenType.Colon))
            {
                Console.WriteLine("Expected ':'");
                return new Literal(0);
            }

            Expression right = MatchTernary();

            return new Ternary(condition, left, right);
        }

        return condition;
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
        else if (Match(TokenType.OpenParen))
        {
            Expression expr = MatchExpression();
            if (!Match(TokenType.CloseParen))
            {
                Console.WriteLine("Unmatched '('");
                return new Literal(0);
            }
            
            return expr;
        }
        else {
            Console.WriteLine("Unexpected token :(");
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
