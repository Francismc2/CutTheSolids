using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("SlerpUnclamped")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion SlerpUnclamped")]
    public class QuaternionSlerpUnclamped : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion a;
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion b;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float t;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            t = GetInputValue(nameof(t), t);

            output = Quaternion.SlerpUnclamped(a, b, t);
            return output;
        }
    }
}