using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("SetFromToRotation")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaterion SetFromToRotation")]
    public class QuaternionSetFromToRotation : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 from;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 to;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion output;

        public override object GetValue(NodePort port)
        {
            from = GetInputValue(nameof(from), from);
            to = GetInputValue(nameof(to), to);

            output.SetFromToRotation(from, to);
            return output;
        }
    }
}