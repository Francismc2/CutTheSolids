using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Normalized")]
    public class QuaternionNormalized : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternion;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion output;

        public override object GetValue(NodePort port)
        {
            quaternion = GetInputValue(nameof(quaternion), quaternion);

            output = Quaternion.Normalize(quaternion);
            return output;
        }
    }
}