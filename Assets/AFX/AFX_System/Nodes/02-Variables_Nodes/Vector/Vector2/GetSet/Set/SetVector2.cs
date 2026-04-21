using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Vector2 + "Set Vector2")]
    public class SetVector2 : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector2Component vector2Component;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector2 valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Vector2 vector2Out;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            vector2Component = GetInputValue(nameof(vector2Component), vector2Component);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            vector2Out = valueIn;
            vector2Component.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return vector2Out;
        }
    }
}