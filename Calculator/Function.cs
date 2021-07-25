using System;

namespace Calculator
{
    // Contains a single function
    public class Function
    {
        // Function delegate
        public delegate double FunctionDelegate(double[] parameters);

        // The function's parameter count
        public int ParameterCount => _paramCount;

        private int _paramCount;

        // The function to execute
        private FunctionDelegate functionDelegate;

        public Function(int paramCount, FunctionDelegate functionDelegate)
        {
            _paramCount = paramCount;
            this.functionDelegate = functionDelegate;
        }

        // Execute
        public double Invoke(double[] parameters)
        {
            // Throw error on parameter count mismatch
            if (parameters.Length != _paramCount)
            {
                throw new Exception($"Invalid parameter count to call {functionDelegate}");
            }

            return functionDelegate.Invoke(parameters);
        }
    }
}
