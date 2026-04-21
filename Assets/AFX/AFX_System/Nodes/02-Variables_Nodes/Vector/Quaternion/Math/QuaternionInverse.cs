using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("Inverse")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Inverse")]
    public class QuaternionInverse : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternion;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion output;

        public override object GetValue(NodePort port)
        {
            quaternion = GetInputValue(nameof(quaternion), quaternion);
            output = Quaternion.Inverse(quaternion);

            return output;
        }
    }
}