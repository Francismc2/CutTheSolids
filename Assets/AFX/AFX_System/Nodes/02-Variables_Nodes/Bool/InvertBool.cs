using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Bool + "Invert Bool")]
    public class InvertBool : AFXNode
    {
        [SerializeField]
        [Input] private bool input;

        [SerializeField]
        [Output] private bool output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return !input;
        }
    }
}