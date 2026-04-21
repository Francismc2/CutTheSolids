using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Atan")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Atan")]
    public class Atan : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.Atan(input);
        }
    }
}