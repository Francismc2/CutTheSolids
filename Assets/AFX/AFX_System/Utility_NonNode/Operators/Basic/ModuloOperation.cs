using System;

namespace Engage.AFX.v1.Math.Operation
{
    public class ModuloOperation : IOperationProvider
    {
        public IOperation GetOperation(object a, object b)
        {
            return (a, b) switch
            {
                (float, float) => new ModuloFloat(),
                (int, float) => new ModuloFloat(),
                (float, int) => new ModuloFloat(),
                (int, int) => new ModuloInt(),
                _ => null
            };
        }

        public Type GetOutputType(Type a, Type b)
        {
            if (a == typeof(float) && b == typeof(float)) return typeof(float);
            if (a == typeof(int) && b == typeof(float)) return typeof(float);
            if (a == typeof(float) && b == typeof(int)) return typeof(float);
            if (a == typeof(int) && b == typeof(int)) return typeof(int);
            return null;
        }

        private class ModuloFloat : IOperation
        {
            public Type OutputType => typeof(float);
            public object DoOperation(object a, object b)
            {
                if (a is float && b is float) return (float)a % (float)b;
                else return a is int ? (int)a % (float)b : (float)a % (int)b;
            }
        }

        private class ModuloInt : IOperation
        {
            public Type OutputType => typeof(int);
            public object DoOperation(object a, object b)
            {
                return (int)a % (int)b;
            }
        }
    }
}