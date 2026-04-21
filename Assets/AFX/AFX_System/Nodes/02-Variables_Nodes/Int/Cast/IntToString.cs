using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.IntCast + "Cast Int to String")]
    public class IntToString : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int input;

        [SerializeField]
        [Output] private string output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return input.ToString();
        }
    }
}