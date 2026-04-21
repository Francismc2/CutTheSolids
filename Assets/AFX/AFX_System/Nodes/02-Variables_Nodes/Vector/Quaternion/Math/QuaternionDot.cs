using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("Dot")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Dot")]
    public class QuaternionDot : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion a;
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion b;

        [SerializeField] [Output] private float output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            return Quaternion.Dot(a, b);
        }
    }
}