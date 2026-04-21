using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Approximately")]
    public class Approximately : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float a;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float b;

        [SerializeField]
        [Output] private bool output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);

            return Mathf.Approximately(a, b);
        }
    }
}