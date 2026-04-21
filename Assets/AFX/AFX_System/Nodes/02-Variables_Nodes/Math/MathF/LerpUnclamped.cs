using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("LerpUnclamped")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF LerpUnclamped")]
    public class LerpUnclamped : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float a;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float b;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float t;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            t = GetInputValue(nameof(t), t);

            return Mathf.LerpUnclamped(a, b, t);
        }
    }
}