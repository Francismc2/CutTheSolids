using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Int + "Set Int")]
    public class SetInt : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private IntComponent intComponent;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private int output;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            intComponent = GetInputValue(nameof(intComponent), intComponent);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            intComponent.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            intComponent = GetInputValue(nameof(intComponent), intComponent);
            return intComponent.Value;
        }
    }
}
