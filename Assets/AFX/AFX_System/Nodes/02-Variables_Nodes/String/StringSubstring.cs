using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Substring")]
    public class StringSubstring : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int index;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int count;

        [SerializeField][Output] private string output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            index = GetInputValue(nameof(index), index);
            count = GetInputValue(nameof(count), count);
            
            return input.Substring(index, count);
        }
    }
}