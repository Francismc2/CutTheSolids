using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Float + "Invert Float")]
    public class InvertFloat : AFXNode
    {
        [SerializeField]
        [Input] private float input;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return -input;
        }
    }
}