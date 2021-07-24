namespace Calculator
{
    // The token struct, constructed by the lexer
    public struct Token
    {
        // Getters to avoid messing up those values
        public TokenType Type => _type;
        public string Value => _value;
        public int Position => _position;

        // The token's type
        private TokenType _type;

        // The token's value
        private string _value;

        // The token's position
        private int _position;

        public Token(TokenType type, string value, int position)
        {
            _type = type;
            _value = value;
            _position = position;
        }

        public override string ToString()
        {
            return $"Token({_type}, {_value})";
        }
    }
}
