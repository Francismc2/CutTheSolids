using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Comparison + "Greater Than")]
    public class GreaterThan : AFXNode
    {
        private const string PortNameIn1 = "A";
        private const string PortNameIn2 = "B";
        private NodePort port1;
        private NodePort port2;

        [SerializeField]
        [Output] private bool output = false;

        protected override void Init()
        {
            this.InitPort<object>(PortNameIn1);
            this.InitPort<object>(PortNameIn2);
            port1 = GetPort(PortNameIn1);
            port2 = GetPort(PortNameIn2);
        }

        public override object GetValue(NodePort port)
        {
            var a = GetPort(PortNameIn1).GetInputValue();
            var b = GetPort(PortNameIn2).GetInputValue();

            if (a == null || b == null) return null;
            if (a is float || b is float)
            {
                return (float)a > (float)b;
            }
            else
            {
                return (int)a > (int)b;
            }
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            Error = null;
            base.OnCreateConnection(from, to);
            if (to.fieldName == PortNameIn1 || to.fieldName == PortNameIn2)
            {
                object a = port1.GetInputValue();
                object b = port2.GetInputValue();
                if ((a == null || b == null)) return;
                if (a is float || a is int)
                {
                    if (b is float || b is int)
                    {
                        return;
                    }
                }

                Error = "Unsupported Types";
            }
        }
    }
}