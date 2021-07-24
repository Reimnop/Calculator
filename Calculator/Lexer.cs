using System;
using System.Text;

namespace Calculator
{
    public class Lexer
    {
        private string text;
        private char? currentChar;

        private int readPos = 0;

        public Lexer(string text)
        {
            this.text = text;

            // Fetch the first character
            Advance();
        }

        // Fetch the next token
        public Token GetNextToken()
        {
            // If it's not the end of the string
            while (currentChar != null)
            {
                // Skip whitespaces
                if (char.IsWhiteSpace((char)currentChar))
                {
                    Advance();
                    continue;
                }

                // Detect numbers
                if (char.IsDigit((char)currentChar))
                {
                    return Number();
                }

                // Detect identifiers
                if (char.IsLetter((char)currentChar))
                {
                    return Identifier();
                }

                // Detect single character tokens
                if (Enum.IsDefined(typeof(TokenType), (int)currentChar))
                {
                    TokenType type = (TokenType)currentChar;
                    Token token = new Token(type, currentChar.ToString(), readPos);

                    Advance();

                    return token;
                }

                throw new LexerException($"Invalid syntax at position {readPos}");
            }

            // The lexer has reached the end of the string
            return new Token(TokenType.EOF, null, readPos);
        }

        // Read numbers
        private Token Number()
        {
            StringBuilder stringBuiler = new StringBuilder();
            while (currentChar != null && (char.IsDigit((char)currentChar) || currentChar == '.'))
            {
                stringBuiler.Append((char)currentChar);
                Advance();
            }
            return new Token(TokenType.Number, stringBuiler.ToString(), readPos);
        }

        // Read identifiers
        private Token Identifier()
        {
            StringBuilder stringBuiler = new StringBuilder();
            while (currentChar != null && char.IsLetterOrDigit((char)currentChar))
            {
                stringBuiler.Append((char)currentChar);
                Advance();
            }
            return new Token(TokenType.Identifier, stringBuiler.ToString(), readPos);
        }

        // Advance the read position
        private void Advance()
        {
            // If there is nothing more to read
            if (readPos >= text.Length)
            {
                currentChar = null;
            }
            else 
            {
                currentChar = text[readPos];
                readPos++;
            }
        }
    }
}
