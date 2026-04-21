using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Comparison + "Equal")]
    public class Equal : AFXNode
    {
        private const string PortNameIn1 = "A";
        private const string PortNameIn2 = "B";

        [SerializeField] [Output] private bool output = false;

        protected override void Init()
        {
            this.InitPort<object>(PortNameIn1);
            this.InitPort<object>(PortNameIn2);
        }

        public override object GetValue(NodePort port)
        {
            var a = GetPort(PortNameIn1).GetInputValue();
            var b = GetPort(PortNameIn2).GetInputValue();

            if (a is float aFloat && b is float bFloat) return Mathf.Approximately(aFloat, bFloat);
            if (a is Vector3 aVec3 && b is Vector3 bVec3)
            {
                if (aVec3 == bVec3) return true;
                return false; 
            }

            if (a is Quaternion aQuat&& b is Quaternion bQuat)
            {
                if (aQuat == bQuat) return true;
                return false;
            }

            return a.Equals(b);
        }
    }
}