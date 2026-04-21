using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Starts With")]
    public class StringStartsWith : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string startsWith;

        [SerializeField][Output] private bool result;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            startsWith = GetInputValue(nameof(startsWith), startsWith);

            return input.StartsWith(startsWith);
        }
    }
}