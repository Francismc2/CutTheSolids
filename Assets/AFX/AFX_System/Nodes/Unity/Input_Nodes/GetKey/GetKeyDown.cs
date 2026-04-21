using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetKeyDown")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input GetKeyDown")]
    public class GetKeyDown : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string keyName;

        [SerializeField]
        [Output] private bool boolOut = false;

        public override object GetValue(NodePort port)
        {
            keyName = GetInputValue(nameof(keyName), keyName);
            return Input.GetKeyDown(keyName);
        }
    }
}