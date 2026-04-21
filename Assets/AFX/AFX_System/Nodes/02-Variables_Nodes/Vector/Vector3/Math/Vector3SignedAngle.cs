using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SignedAngle")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 SignedAngle")]
    public class Vector3SignedAngle : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 from;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 to;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 axis;


        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            from = GetInputValue(nameof(from), from);
            to = GetInputValue(nameof(to), to);
            axis = GetInputValue(nameof(axis), axis);

            return Vector3.SignedAngle(from, to, axis);
        } 
    }
}