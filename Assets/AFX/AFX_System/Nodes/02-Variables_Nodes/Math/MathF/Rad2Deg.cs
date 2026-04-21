using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Rad2Deg")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Rad2Deg")]
    public class Rad2Deg : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float rad;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            rad = GetInputValue(nameof(rad), rad);

            return rad * Mathf.Rad2Deg;
        }
    }
}