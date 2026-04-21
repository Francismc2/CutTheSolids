using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("RotateTowards")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion RotateTowards")]
    public class QuaternionRotateTowards : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion from;
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion to;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float maxDelta;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion output;

        public override object GetValue(NodePort port)
        {
            from = GetInputValue(nameof(from), from);
            to = GetInputValue(nameof(to), to);
            maxDelta = GetInputValue(nameof(maxDelta), maxDelta);

            output = Quaternion.RotateTowards(from, to, maxDelta);
            return output;
        }
    }
}