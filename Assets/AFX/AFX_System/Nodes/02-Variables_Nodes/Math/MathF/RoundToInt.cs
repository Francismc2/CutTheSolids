using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("RoundToInt")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF RoundToInt")]
    public class RoundToInt : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float f;
        
        [SerializeField][Output] private int output;

        public override object GetValue(NodePort port)
        {
            f = GetInputValue(nameof(f), f);

            return Mathf.RoundToInt(f);
        }
    }
}