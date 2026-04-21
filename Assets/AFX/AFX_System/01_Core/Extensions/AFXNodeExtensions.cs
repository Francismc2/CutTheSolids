using XNode;
using System;

namespace Engage.AFX.v1
{
    public static class AFXNodeExtensions
    {
        public static NodePort InitPort<PortType>(this AFXNode node, string portName)
        {
            if (node.GetInputPort(portName) != null) return node.GetInputPort(portName);

            return node.AddDynamicInput(
                typeof(PortType),
                connectionType: Node.ConnectionType.Override,
                typeConstraint: Node.TypeConstraint.None,
                fieldName: portName);
        }

        public static NodePort SwapDynamicInputPortWithNewType(this Node node, string portName, Type newType)
        {
            if (!node.HasPort(portName)) return node.AddDynamicInput(newType, fieldName: portName);

            NodePort temp = node.GetPort(portName);
            if (temp.ValueType == newType)
            {
                return temp;
            }

            NodePort OtherNode = temp;
            if (temp.ConnectionCount > 0)
            {
                OtherNode = temp.Connection;
            }

            node.RemoveDynamicPort(temp);
            NodePort newInput = node.AddDynamicInput(newType, fieldName: portName);
            if (OtherNode != temp && OtherNode.ValueType == newType)
            {
                newInput.Connect(OtherNode);
            }

            return newInput;
        }

        public static NodePort SwapDynamicOutputPortWithNewType(this Node node, string portName, Type newType)
        {
            if (!node.HasPort(portName)) return node.AddDynamicOutput(newType, fieldName: portName);

            NodePort temp = node.GetPort(portName);
            if (temp.ValueType == newType)
            {
                return temp;
            }

            node.RemoveDynamicPort(portName);

            return node.AddDynamicOutput(newType, fieldName: portName);
        }
    }
}