using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetKey")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input GetKey")]
    public class GetKey : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string keyName;

        [SerializeField]
        [Output] private bool boolOut = false;

        public override object GetValue(NodePort port)
        {
            keyName = GetInputValue(nameof(keyName), keyName);
            return Input.GetKey(keyName);
        }
    }
}