using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("FloorToInt")]
    [CreateNodeMenu(AFXMenuTree.MathF + "Mathf FloorToInt")]
    public class FloorToInt : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField][Output] private int output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.FloorToInt(input);
        }
    }
}