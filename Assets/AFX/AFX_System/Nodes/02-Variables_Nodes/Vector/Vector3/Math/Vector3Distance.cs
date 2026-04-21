using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Distance")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Distance")]
    public class Vector3Distance : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 a;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 b;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);

            return Vector3.Distance(a, b);
        }
    }
}