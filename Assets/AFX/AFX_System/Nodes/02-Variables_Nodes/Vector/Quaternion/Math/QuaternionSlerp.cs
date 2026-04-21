using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("Slerp")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Slerp")]
    public class QuaternionSlerp : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion a;
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion b;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float t;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternion;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            t = GetInputValue(nameof(t), t);

            quaternion = Quaternion.Slerp(a, b, t);

            return quaternion;
        }
    }
}