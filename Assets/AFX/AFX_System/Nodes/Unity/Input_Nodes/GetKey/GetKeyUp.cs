using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetKeyUp")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input GetKeyUp")]
    public class GetKeyUp : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string keyName;

        [SerializeField]
        [Output] private bool boolOut = false;

        public override object GetValue(NodePort port)
        {
            keyName = GetInputValue(nameof(keyName), keyName);
            return Input.GetKeyUp(keyName);
        }
    }
}