using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Ends With")]
    public class StringEndsWith : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string endsWith;

        [SerializeField][Output] private bool result;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            endsWith = GetInputValue(nameof(endsWith), endsWith);

            return input.EndsWith(endsWith);
        }
    }
}