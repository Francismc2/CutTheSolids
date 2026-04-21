using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Cross")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Cross")]
    public class Vector3Cross : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 lhs;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 rhs;

        [SerializeField]
        [Output] private Vector3 output;

        public override object GetValue(NodePort port)
        {
            lhs = GetInputValue(nameof(lhs), lhs);
            rhs = GetInputValue(nameof(rhs), rhs);

            return Vector3.Cross(lhs, rhs);
        } 
    }
}