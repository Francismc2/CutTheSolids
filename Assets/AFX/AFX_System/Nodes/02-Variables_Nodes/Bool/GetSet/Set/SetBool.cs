using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Bool + "Set Bool")]
    public class SetBool : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private BoolComponent boolComponent;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private bool output;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            boolComponent = GetInputValue(nameof(boolComponent), boolComponent);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            boolComponent.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            boolComponent = GetInputValue(nameof(boolComponent), boolComponent);
            return boolComponent.Value;
        }
    }
}
