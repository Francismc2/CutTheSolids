using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Ceil")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Ceil")]
    public class Ceil : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.Ceil(input);
        }
    }
}