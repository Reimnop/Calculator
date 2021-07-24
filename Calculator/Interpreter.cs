namespace Calculator
{
    // Since this is a very simple calculator, an Abstract Syntax Tree is not required and is not implemented to maximize speed
    // The parser and interpreter are merged together
    public class Interpreter
    {
        // The lexer handles splitting the string into tokens
        private Lexer lexer;

        // The function solver handles solving functions like sin, cos, tan,...
        private FunctionSolver functionSolver;

        // The current token
        private Token currentToken;

        public Interpreter(string text)
        {
            lexer = new Lexer(text);
            functionSolver = new FunctionSolver();

            // Fetch the first token
            currentToken = lexer.GetNextToken();
        }

        // Do I even need to explain this?
        public double Evaluate()
        {
            return Expr();
        }

        private double Expr()
        {
            // Expr : Term ((Plus | Minus) Term)*

            double result = Term();

            while (currentToken.Type == TokenType.Plus || currentToken.Type == TokenType.Minus)
            {
                Token token = currentToken;
                Eat(currentToken.Type);

                switch (token.Type)
                {
                    case TokenType.Plus:
                        result += Term();
                        break;
                    case TokenType.Minus:
                        result -= Term();
                        break;
                }
            }

            return result;
        }

        private double Term()
        {
            // Term : Factor ((Mult | Div) Factor)*

            double result = Factor();

            while (currentToken.Type == TokenType.Mult || currentToken.Type == TokenType.Div)
            {
                Token token = currentToken;
                Eat(currentToken.Type);

                switch (token.Type)
                {
                    case TokenType.Mult:
                        result *= Factor();
                        break;
                    case TokenType.Div:
                        result /= Factor();
                        break;
                }
            }

            return result;
        }

        private double Factor()
        {
            // Factor : Number | Unary | OpenParen Expr CloseParen

            switch (currentToken.Type)
            {
                case TokenType.Number:
                    Token token = currentToken;
                    Eat(TokenType.Number);
                    return double.Parse(token.Value);
                case TokenType.Plus:
                case TokenType.Minus:
                    return Unary();
                case TokenType.OpenParen:
                    Eat(TokenType.OpenParen);
                    double result = Expr();
                    Eat(TokenType.CloseParen);
                    return result;
                case TokenType.Identifier:
                    return Function();
            }

            // Throw an exception due to unexpected token
            throw new ParserException($"Unexpected token: expected Number, got {currentToken.Type} at position {currentToken.Position}");
        }

        private double Unary()
        {
            // Unary : Plus Factor | Minus Factor
            
            if (currentToken.Type == TokenType.Plus || currentToken.Type == TokenType.Minus)
            {
                Token token = currentToken;
                Eat(currentToken.Type);

                switch (token.Type)
                {
                    case TokenType.Plus:
                        return +Factor();
                    case TokenType.Minus:
                        return -Factor();
                }
            }

            // Throw an exception due to unexpected token
            // You can't even get here normally
            throw new ParserException($"Unexpected token: expected Plus, got {currentToken.Type} at position {currentToken.Position}");
        }

        private double Function()
        {
            // Function : Identifier OpenParen Expr CloseParen

            Token token = currentToken;
            Eat(TokenType.Identifier);

            // Throw error if function doesn't exist
            if (!functionSolver.ContainsFunction(token.Value))
            {
                throw new InterpreterException($"The function \"{token.Value}\" does not exist! (at position {token.Position})");
            }

            Eat(TokenType.OpenParen);
            double x = Expr();
            Eat(TokenType.CloseParen);

            return functionSolver.Solve(token.Value, x);
        }

        // Consumes the current token
        private void Eat(TokenType type)
        {
            if (currentToken.Type == type)
            {
                // Fetch the next token
                currentToken = lexer.GetNextToken();
            }
            else
            {
                // Unexpected token
                throw new ParserException($"Unexpected token: expected {type}, got {currentToken.Type} at position {currentToken.Position}");
            }
        }
    }
}