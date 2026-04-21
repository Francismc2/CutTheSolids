using UnityEngine;
using XNode;
using System;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Abs")]
    public class Abs : AFXNode
    {
        private const string PortNameIn1 = "A";
        private const string PortNameOut = "Output";

        private NodePort port1;
        private NodePort outputPort;

        protected override void Init()
        {
            port1 = this.InitPort<object>(PortNameIn1);
            outputPort = GetOutputPort(PortNameOut);
        }

        public override object GetValue(NodePort port)
        {
            return outputPort.ValueType switch
            {
                Type t when t == typeof(float) => Mathf.Abs((float)port1.GetInputValue()),
                Type t when t == typeof(int) => Mathf.Abs((int)port1.GetInputValue()),
                _ => null
            };
        }
        private void SetupPorts()
        {
            Error = null;
            NodePort a = port1.Connection;
            if (a == null) return;

            if (a.ValueType != typeof(float) && a.ValueType != typeof(int))
            {
                Error = "Unsupported Types";
                return;
            }

            this.SwapDynamicOutputPortWithNewType(PortNameOut, port1.Connection.ValueType);
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName == PortNameIn1)
            {
                SetupPorts();
            }
        }
    }
}