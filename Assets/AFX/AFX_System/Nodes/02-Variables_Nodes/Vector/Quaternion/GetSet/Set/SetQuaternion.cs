using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Set Quaternion")]
    public class SetQuaternion : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private QuaternionComponent quaternionComponent;
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Quaternion valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Quaternion quaternionOut;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            quaternionComponent = GetInputValue(nameof(quaternionComponent), quaternionComponent);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            quaternionOut = valueIn;
            quaternionComponent.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return quaternionOut;
        }
    }
}
