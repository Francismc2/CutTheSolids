using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Max")]
    public class Max : AFXNode
    {
        private const string PortNameIn1 = "A";
        private const string PortNameIn2 = "B";
        private const string PortNameOut = "Output";

        private NodePort port1;
        private NodePort port2;

        NodePort a;
        NodePort b;

        protected override void Init()
        {
            port1 = this.InitPort<object>(PortNameIn1);
            port2 = this.InitPort<object>(PortNameIn2);
        }

        public override object GetValue(NodePort port)
        {
            object a = port1.GetInputValue();
            object b = port2.GetInputValue();

            if (a == null || b == null) return null;
            if (a is float || b is float)
            {
                return Mathf.Max((float)port1.GetInputValue(), (float)port2.GetInputValue());
            }
            else
            {
                return Mathf.Max((int)port1.GetInputValue(), (int)port2.GetInputValue());
            }
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName == PortNameIn1 || to.fieldName == PortNameIn2)
            {
                a = port1.Connection;
                b = port2.Connection;

                if (!ConnectionTypeCheck())
                {
                    Error = "Unsupported Types";
                    return;
                }

                if (a == null || b == null) return;

                if (a.ValueType == typeof(float) || b.ValueType == typeof(float))
                {
                    this.SwapDynamicOutputPortWithNewType(PortNameOut, typeof(float));
                    return;
                }

                if (a.ValueType == typeof(int) && b.ValueType == typeof(int))
                {
                    this.SwapDynamicOutputPortWithNewType(PortNameOut, typeof(int));
                }
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            if (ConnectionTypeCheck())
            {
                Error = null;
            }
        }

        private bool ConnectionTypeCheck()
        {
            return IsValidType(a) && IsValidType(b);
        }

        private static bool IsValidType(NodePort port)
        {
            if (port == null) return true;
            if (!port.IsConnected) return true;
            if (port.ValueType == typeof(float) || port.ValueType == typeof(int)) return true;
            return false;
        }
    }
}