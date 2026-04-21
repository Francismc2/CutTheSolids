using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("ClosestPowerOfTwo")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF ClosestPowerOfTwo")]
    public class ClosestPowerOfTwo : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int input;
        
        [SerializeField][Output] private int output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.ClosestPowerOfTwo(input);
        }
    }
}