using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Slerp")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Slerp")]
    public class Vector3Slerp : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 a;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 b;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float t;

        [SerializeField]
        [Output] private Vector3 output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);
            t = GetInputValue(nameof(t), t);

            return Vector3.Slerp(a, b, t);
        } 
    }
}