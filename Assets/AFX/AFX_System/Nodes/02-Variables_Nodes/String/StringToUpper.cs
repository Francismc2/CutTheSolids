using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String To Upper")]
    public class StringToUpper : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;

        [SerializeField][Output] private string stringUpper;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return input.ToUpper();
        }
    }
}