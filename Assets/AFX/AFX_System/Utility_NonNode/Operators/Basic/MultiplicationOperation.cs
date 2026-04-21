using System;
using UnityEngine;

namespace Engage.AFX.v1.Math.Operation
{
    public class MultiplicationOperation : IOperationProvider
    {
        public IOperation GetOperation(object a, object b)
        {
            return (a, b) switch
            {
                (float, float) => new MultiplyFloat(),
                (int, float) => new MultiplyFloat(),
                (float, int) => new MultiplyFloat(),
                (int, int) => new MultiplyInt(),
                (float, Vector3) => new MultiplyVector3(),
                (Vector3, float) => new MultiplyVector3(),
                (Quaternion, Quaternion) => new MultiplyQuaterion(),
                (Quaternion, Vector3) => new MultiplyQuaterion(),
                _ => null
            };
        }

        public Type GetOutputType(Type a, Type b)
        {
            if (a == typeof(float) && b == typeof(float)) return typeof(float);
            if (a == typeof(int) && b == typeof(float)) return typeof(float);
            if (a == typeof(float) && b == typeof(int)) return typeof(float);
            if (a == typeof(int) && b == typeof(int)) return typeof(int);
            if (a == typeof(float) && b == typeof(Vector3)) return typeof(Vector3);
            if (a == typeof(Vector3) && b == typeof(float)) return typeof(Vector3);
            if (a == typeof(Quaternion) && b == typeof(Quaternion)) return typeof(Quaternion);
            if (a == typeof(Quaternion) && b == typeof(Vector3)) return typeof(Vector3);
            return null;
        }

        private class MultiplyFloat : IOperation
        {
            public Type OutputType => typeof(float);
            public object DoOperation(object a, object b)
            {
                if (a is float && b is float) return (float)a * (float)b;
                else return a is int ? (int)a * (float)b : (float)a * (int)b;
            }
        }

        private class MultiplyInt : IOperation
        {
            public Type OutputType => typeof(int);
            public object DoOperation(object a, object b) => (int)a * (int)b;
        }

        private class MultiplyVector3 : IOperation
        {
            public Type OutputType => typeof(Vector3);
            public object DoOperation(object a, object b) => a is float ? (float)a * (Vector3)b : (Vector3)a * (float)b;
        }

        private class MultiplyQuaterion : IOperation
        {
            public Type OutputType => typeof(Quaternion);
            public object DoOperation(object a, object b) => b is Quaternion ? (Quaternion)a * (Quaternion)b : ((Quaternion)a * (Vector3)b);

        }
    }
}