using System;

namespace Engage.AFX.v1.Math.Operation
{
    public interface IOperation
    {
        Type OutputType { get; }
        object DoOperation(object a, object b);
    }
}