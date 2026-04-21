using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Int + "Invert Int")]
    public class InvertInt : AFXNode
    {
        [SerializeField]
        [Input] private int input;

        [SerializeField]
        [Output] private int output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return -input;
        }
    }
}