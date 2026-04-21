using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Clamp01")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Clamp01")]
    public class Clamp01 : AFXNode
    {
        private const string PortNameIn = "Value";
        private const string PortNameOut = "Output";

        private NodePort port1;

        NodePort a;

        protected override void Init()
        {
            port1 = this.InitPort<object>(PortNameIn);
        }

        public override object GetValue(NodePort port)
        {
            object a = port1.GetInputValue();

            if (a == null) return null;

            if (a is float aFloat)
            {
                return Mathf.Clamp01(aFloat);
            }

            if (a is int aInt)
            {
                var clampedValue = Mathf.Clamp01(aInt);
                return Mathf.RoundToInt(clampedValue);
            }

            return a;
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName == PortNameIn)
            {
                a = port1.Connection;

                if (!ConnectionTypeCheck())
                {
                    Error = "Unsupported Types";
                    return;
                }

                if (a == null) return;

                if (a.ValueType == typeof(float))
                {
                    this.SwapDynamicOutputPortWithNewType(PortNameOut, typeof(float));
                    return;
                }

                if (a.ValueType == typeof(int))
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
            return IsValidType(a);
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