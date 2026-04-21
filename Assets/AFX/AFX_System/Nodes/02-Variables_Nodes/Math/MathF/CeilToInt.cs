using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("CeilToInt")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF CeilToInt")]
    public class CeilToInt : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField][Output] private int output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.CeilToInt(input);
        }
    }
}