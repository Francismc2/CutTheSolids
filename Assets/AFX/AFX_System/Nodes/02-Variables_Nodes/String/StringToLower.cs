using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String To Lower")]
    public class StringToLower : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;

        [SerializeField][Output] private string stringLower;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return input.ToLower();
        }
    }
}