using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Clamp")]
    public class Clamp : AFXNode
    {
        private const string PortNameIn1 = "Value";
        private const string PortNameIn2 = "Min";
        private const string PortNameIn3 = "Max";
        private const string PortNameOut = "Output";

        private NodePort port1;
        private NodePort port2;
        private NodePort port3;

        NodePort a;
        NodePort b;
        NodePort c;

        protected override void Init()
        {
            port1 = this.InitPort<object>(PortNameIn1);
            port2 = this.InitPort<object>(PortNameIn2);
            port3 = this.InitPort<object>(PortNameIn3);
        }

        public override object GetValue(NodePort port)
        {
            object a = port1.GetInputValue();
            object b = port2.GetInputValue();
            object c = port3.GetInputValue();

            if (a == null || b == null || c == null) return null;
            if (a is float || b is float || c is float)
            {
                return Mathf.Clamp((float)port1.GetInputValue(), (float)port2.GetInputValue(), (float)port3.GetInputValue());
            }
            else
            {
                return Mathf.Clamp((int)port1.GetInputValue(), (int)port2.GetInputValue(), (int)port3.GetInputValue());
            }
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName == PortNameIn1 || to.fieldName == PortNameIn2 || to.fieldName == PortNameIn3)
            {
                a = port1.Connection;
                b = port2.Connection;
                c = port3.Connection;

                if (!ConnectionTypeCheck())
                {
                    Error = "Unsupported Types";
                    return;
                }

                if (a == null || b == null || c == null) return;

                if (a.ValueType == typeof(float) || b.ValueType == typeof(float) || c.ValueType == typeof(float))
                {
                    this.SwapDynamicOutputPortWithNewType(PortNameOut, typeof(float));
                    return;
                }

                if (a.ValueType == typeof(int) && b.ValueType == typeof(int) && c.ValueType == typeof(int))
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
            return IsValidType(a) && IsValidType(b) && IsValidType(c);
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