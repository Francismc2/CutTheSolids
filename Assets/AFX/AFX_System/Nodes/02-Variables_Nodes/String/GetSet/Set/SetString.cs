using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "Set String")]
    public class SetString : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private StringComponent stringComponent;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private string output;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            stringComponent = GetInputValue(nameof(stringComponent), stringComponent);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            output = valueIn;
            stringComponent.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}