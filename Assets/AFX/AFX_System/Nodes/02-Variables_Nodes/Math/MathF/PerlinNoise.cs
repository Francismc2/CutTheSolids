using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("PerlinNoise")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF PerlinNoise")]
    public class PerlinNoise : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float x;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float y;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            x = GetInputValue(nameof(x), x);
            y = GetInputValue(nameof(y), y);

            return Mathf.PerlinNoise(x, y);
        }
    }
}