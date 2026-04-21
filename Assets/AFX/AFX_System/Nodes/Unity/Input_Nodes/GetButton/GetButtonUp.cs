using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetButtonUp")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input Get Button Up")]
    public class GetButtonUp : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string buttonName;

        [SerializeField]
        [Output] private bool boolOut = false;

        public override object GetValue(NodePort port)
        {
            buttonName = GetInputValue(nameof(buttonName), buttonName);
            return Input.GetButtonUp(buttonName);
        }
    }
}