using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Angle")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Angle")]
    public class Vector3Angle : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 from;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 to;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            from = GetInputValue(nameof(from), from);
            to = GetInputValue(nameof(to), to);

            return Vector3.Angle(from, to);
        } 
    }
}