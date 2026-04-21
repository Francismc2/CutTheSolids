using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Vector3")]
    [CreateNodeMenu(AFXMenuTree.Vector3 + "Set Vector3")]
    public class SetVector3 : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector3Component vector3Component;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 valueIn;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Vector3 vector3Out;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            vector3Component = GetInputValue(nameof(vector3Component), vector3Component);
            valueIn = GetInputValue(nameof(valueIn), valueIn);
            vector3Out = valueIn;
            vector3Component.Value = valueIn;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return vector3Out;
        }
    }
}