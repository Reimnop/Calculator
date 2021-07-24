using System;

namespace Calculator
{
    internal class InterpreterException : Exception
    {
        public InterpreterException(string message) : base(message)
        {
        }
    }
}
