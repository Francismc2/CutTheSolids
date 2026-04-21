using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Split Quaternion")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Split Quaternion")]
    public class QuaternionSplitter : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Quaternion quaternionIn;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private float x;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float y;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float z;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float w;

        public override object GetValue(NodePort port)
        {
            quaternionIn = GetInputValue(nameof(quaternionIn), quaternionIn);
            if (port.fieldName == nameof(x))
            {
                return quaternionIn.x;
            }
            if (port.fieldName == nameof(y))
            {
                return quaternionIn.y;
            }
            if (port.fieldName == nameof(z))
            {
                return quaternionIn.z;
            }
            if (port.fieldName == nameof(w))
            {
                return quaternionIn.w;
            }
            return null;
        }
    }
}