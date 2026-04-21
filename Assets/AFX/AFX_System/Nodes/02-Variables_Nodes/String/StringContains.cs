using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Contains")]
    public class StringContains : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string contains;

        [SerializeField][Output] private bool result;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            contains = GetInputValue(nameof(contains), contains);

            return input.Contains(contains);
        }
    }
}