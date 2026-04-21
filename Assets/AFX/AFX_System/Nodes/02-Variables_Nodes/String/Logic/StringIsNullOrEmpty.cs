using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("IsNullOrEmpty")]
    [CreateNodeMenu(AFXMenuTree.StringLogic + "Logic String IsNullOrEmpty")]
    public class StringIsNullOrEmpty : AFXNode
    {
        [SerializeField][Input] private string input;

        [SerializeField][Output] private bool output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return string.IsNullOrEmpty(input);
        }
    }
}