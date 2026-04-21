using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("SetLookRotation")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion SetLookRotation")]
    public class QuaternionSetLookRotation : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 view;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 up;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternion;

        public override object GetValue(NodePort port)
        {
            view = GetInputValue(nameof(view), view);
            up = GetInputValue(nameof(up), up);

            quaternion.SetLookRotation(view, up);
            return quaternion;
        }
    }
}