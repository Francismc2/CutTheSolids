using System;
using UnityEngine;

namespace Engage.AFX.v1.Math.Operation
{
    public class DivisionOperation : IOperationProvider
    {
        public IOperation GetOperation(object a, object b)
        {
            return (a, b) switch
            {
                (float, float) => new DivideFloat(),
                (int, float) => new DivideFloat(),
                (float, int) => new DivideFloat(),
                (int, int) => new DivideInt(),
                (Vector3, float) => new DivideVector3(),
                (Vector3, int) => new DivideVector3(),
                _ => null
            };
        }

        public Type GetOutputType(Type a, Type b)
        {
            if (a == typeof(float) && b == typeof(float)) return typeof(float);
            if (a == typeof(int) && b == typeof(float)) return typeof(float);
            if (a == typeof(float) && b == typeof(int)) return typeof(float);
            if (a == typeof(int) && b == typeof(int)) return typeof(int);
            if (a == typeof(Vector3) && b == typeof(float)) return typeof(Vector3);
            if (a == typeof(Vector3) && b == typeof(int)) return typeof(Vector3);
            return null;
        }

        private class DivideFloat : IOperation
        {
            public Type OutputType => typeof(float);
            public object DoOperation(object a, object b)
            {
                if (a is float aFloat && b is float bFloat)
                {
                    if (bFloat == 0f) return 0;
                    return aFloat / bFloat;
                }

                else
                {
                    if ((float)b == 0) return 0;
                    return a is int ? (int)a / (float)b : (float)a / (int)b;
                }
            }
        }

        private class DivideInt : IOperation
        {
            public Type OutputType => typeof(int);
            public object DoOperation(object a, object b)
            {
                if ((int)b == 0) return 0;
                return (int)a / (int)b;
            }
        }

        private class DivideVector3 : IOperation
        {
            public Type OutputType => typeof(Vector3);
            public object DoOperation(object a, object b) => b is float ? (Vector3)a / (float)b : (Vector3)a / (int)b;
        }
    }
}