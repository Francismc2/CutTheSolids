using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Get Quaternion")]
    public class GetQuaternion : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private QuaternionComponent quaternionComponent;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Quaternion quaternionOut;

        public override object GetValue(NodePort port)
        {
            quaternionOut = GetInputValue(nameof(quaternionComponent), quaternionComponent).Value;
            return quaternionOut;
        }
    }
}