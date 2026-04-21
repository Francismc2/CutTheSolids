using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SmoothStep")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF SmoothStep")]
    public class SmoothStep : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float from;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float to;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float t;
        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            from = GetInputValue(nameof(from), from);
            to = GetInputValue(nameof(to), to);
            t = GetInputValue(nameof(t), t);

            return Mathf.SmoothStep(from, to, t);
        }
    }
}