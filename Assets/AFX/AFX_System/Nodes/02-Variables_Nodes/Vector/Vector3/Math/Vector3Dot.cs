using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Dot")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Dot")]
    public class Vector3Dot : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 lhs;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 rhs;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            lhs = GetInputValue(nameof(lhs), lhs);
            rhs = GetInputValue(nameof(rhs), rhs);

            return Vector3.Dot(lhs, rhs);
        }
    }
}