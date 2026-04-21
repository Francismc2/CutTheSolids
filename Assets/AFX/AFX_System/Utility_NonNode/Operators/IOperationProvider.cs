using System;

namespace Engage.AFX.v1.Math.Operation
{
    public interface IOperationProvider
    {
        IOperation GetOperation(object a, object b);
        public Type GetOutputType(Type a, Type b);
    }
}