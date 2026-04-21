using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("InverseLerp")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF InverseLerp")]
    public class InverseLerp : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float a;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float b;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float value;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            value = GetInputValue(nameof(value), value);

            return Mathf.InverseLerp(a, b, value);
        }
    }
}