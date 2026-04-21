using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("LookRotation")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion LookRotation")]
    public class QuaternionLookRotation : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 forward;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 upward;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternion;

        public override object GetValue(NodePort port)
        {
            forward = GetInputValue(nameof(forward), forward);
            upward = GetInputValue(nameof(upward), upward);

            quaternion = Quaternion.LookRotation(forward, upward);
            return quaternion;
        }
    }
}