using System;
using System.Collections.Generic;

namespace Calculator
{
    // Contains all built-in functions
    public class FunctionSolver
    {
        // Name, Function
        private Dictionary<string, Function> functions = new Dictionary<string, Function>()
        {
            { "sin", new Function(1, (x) => Math.Sin(x[0])) },
            { "cos", new Function(1, (x) => Math.Cos(x[0])) },
            { "tan", new Function(1, (x) => Math.Tan(x[0])) },
            { "asin", new Function(1, (x) => Math.Asin(x[0])) },
            { "acos", new Function(1, (x) => Math.Acos(x[0])) },
            { "atan", new Function(1, (x) => Math.Atan(x[0])) },
            { "abs", new Function(1, (x) => Math.Abs(x[0])) },
            { "floor", new Function(1, (x) => Math.Floor(x[0])) },
            { "ceiling", new Function(1, (x) => Math.Ceiling(x[0])) },
            { "round", new Function(1, (x) => Math.Round(x[0])) },
            { "sqrt", new Function(1, (x) => Math.Sqrt(x[0])) },
            { "pow", new Function(2, (x) => Math.Pow(x[0], x[1])) },
            { "ln", new Function(1, (x) => Math.Log(x[0])) },
            { "log2", new Function(1, (x) => Math.Log2(x[0])) },
            { "log10", new Function(1, (x) => Math.Log10(x[0])) }
        };

        public bool ContainsFunction(string name)
        {
            return functions.ContainsKey(name);
        }

        public double Solve(string name, double[] parameters)
        {
            return functions[name].Invoke(parameters);
        }
    }
}
