using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Float + "Set Float")]
    public class SetFloat : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private FloatComponent floatComponent;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float output;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            floatComponent = GetInputValue(nameof(floatComponent), floatComponent);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            floatComponent.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            floatComponent = GetInputValue(nameof(floatComponent), floatComponent);
            return floatComponent.Value;
        }
    }
}
