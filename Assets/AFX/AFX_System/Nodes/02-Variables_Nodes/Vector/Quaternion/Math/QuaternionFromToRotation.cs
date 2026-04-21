using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("FromToRotation")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion FromToRotation")]
    public class QuaternionFromToRotation : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 from;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 to;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternion;

        public override object GetValue(NodePort port)
        {
            from = GetInputValue(nameof(from), from);
            to = GetInputValue(nameof(to), to);
            
            quaternion = Quaternion.FromToRotation(from, to);
            return quaternion;
        }
    }
}