using System;
using UnityEngine;

namespace Engage.AFX.v1.Math.Operation
{
    public class SubtractionOperation : IOperationProvider
    {
        public IOperation GetOperation(object a, object b)
        {
            return (a, b) switch
            {
                (float, float) => new SubtractFloat(),
                (int, float) => new SubtractFloat(),
                (float, int) => new SubtractFloat(),
                (int, int) => new SubtractInt(),
                (Vector3, Vector3) => new SubtractVector3(),
                _ => null
            };
        }

        public Type GetOutputType(Type a, Type b)
        {
            if (a == typeof(float) && b == typeof(float)) return typeof(float);
            if (a == typeof(int) && b == typeof(float)) return typeof(float);
            if (a == typeof(float) && b == typeof(int)) return typeof(float);
            if (a == typeof(int) && b == typeof(int)) return typeof(int);
            if (a == typeof(Vector3) && b == typeof(Vector3)) return typeof(Vector3);
            return null;
        }

        private class SubtractFloat : IOperation
        {
            public Type OutputType => typeof(float);
            public object DoOperation(object a, object b)
            {
                if (a is float && b is float) return (float)a - (float)b;
                else return a is int ? (int)a - (float)b : (float)a - (int)b;
            }
        }

        private class SubtractInt : IOperation
        {
            public Type OutputType => typeof(int);
            public object DoOperation(object a, object b) => (int)a - (int)b;
        }

        private class SubtractVector3 : IOperation
        {
            public Type OutputType => typeof(Vector3);
            public object DoOperation(object a, object b) => (Vector3)a - (Vector3)b;
        }
    }
}