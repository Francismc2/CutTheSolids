using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Get Normalized")]
    public class GetQuaternionNormalized : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternionIn;
        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternionOut;

        public override object GetValue(NodePort port)
        {
            quaternionIn = GetInputValue(nameof(quaternionIn), quaternionIn);
            quaternionOut = quaternionIn.normalized;

            return quaternionOut;
        }
    }
}