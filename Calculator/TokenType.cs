namespace Calculator
{
    // Contains all token types that are used
    public enum TokenType
    {
        OpenParen      = '(',
        CloseParen     = ')',
        Period         = '.',
        Comma          = ',',
        Plus           = '+',
        Minus          = '-',
        Mult           = '*',
        Div            = '/',

        EOF        = 0,
        Number     = 1,
        Identifier = 2
    }
}