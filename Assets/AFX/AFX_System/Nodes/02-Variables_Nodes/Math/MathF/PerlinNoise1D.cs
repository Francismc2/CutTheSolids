using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("PerlinNoise1D")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF PerlinNoise1D")]
    public class PerlinNoise1D : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float x;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            x = GetInputValue(nameof(x), x);

            return Mathf.PerlinNoise(x,0);
        }
    }
}