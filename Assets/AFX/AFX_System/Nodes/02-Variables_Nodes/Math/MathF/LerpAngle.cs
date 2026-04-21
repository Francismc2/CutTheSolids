using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("LerpAngle")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF LerpAngle")]
    public class LerpAngle : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float a;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float b;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float t;
        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            t = GetInputValue(nameof(t), t);

            return Mathf.LerpAngle(a, b, t);
        }
    }
}