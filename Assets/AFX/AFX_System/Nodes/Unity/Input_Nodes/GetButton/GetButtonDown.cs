using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetButtonDown")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input GetButtonDown")]
    public class GetButtonDown : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string buttonName;

        [SerializeField]
        [Output] private bool boolOut = false;

        public override object GetValue(NodePort port)
        {
            buttonName = GetInputValue(nameof(buttonName), buttonName);
            return Input.GetButtonDown(buttonName);
        }
    }
}