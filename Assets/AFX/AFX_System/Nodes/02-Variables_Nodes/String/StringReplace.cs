using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Replace")]
    public class StringReplace : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string find;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string replace;

        [SerializeField][Output] private string output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            find = GetInputValue(nameof(find), find);
            replace = GetInputValue(nameof(replace), replace);

            return input.Replace(find, replace);
        }
    }
}