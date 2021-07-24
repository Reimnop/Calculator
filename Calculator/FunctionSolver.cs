using System;
using System.Collections.Generic;

namespace Calculator
{
    // Contains all built-in functions
    public class FunctionSolver
    {
        // Function delegate
        private delegate double Function(double x);

        // Name, Function
        private Dictionary<string, Function> functions = new Dictionary<string, Function>()
        {
            { "sin", Math.Sin },
            { "cos", Math.Cos },
            { "tan", Math.Tan },
            { "asin", Math.Asin },
            { "acos", Math.Acos },
            { "atan", Math.Atan },
            { "abs", Math.Abs }
        };

        public bool ContainsFunction(string name)
        {
            return functions.ContainsKey(name);
        }

        public double Solve(string name, double x)
        {
            return functions[name](x);
        }
    }
}
