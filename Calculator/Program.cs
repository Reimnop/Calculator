using System;

namespace Calculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Interpreter interpreter = new Interpreter(input);
            Console.WriteLine(interpreter.Evaluate());
        }
    }
}
