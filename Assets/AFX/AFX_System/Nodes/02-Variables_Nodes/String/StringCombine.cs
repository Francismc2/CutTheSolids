using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "Combine")]
    public class StringCombine : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string a = "";
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string b = "";

        [SerializeField]
        [Output] private string output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            return a + b;
        }
    }
}