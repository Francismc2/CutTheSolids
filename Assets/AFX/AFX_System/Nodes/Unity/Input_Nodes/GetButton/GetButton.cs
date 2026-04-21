using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetButton")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input GetButton")]
    public class GetButton : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string buttonName;

        [SerializeField]
        [Output] private bool boolOut = false;

        public override object GetValue(NodePort port)
        {
            buttonName = GetInputValue(nameof(buttonName), buttonName);
            return Input.GetButton(buttonName);
        }
    }
}