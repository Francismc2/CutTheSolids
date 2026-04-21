using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("ClampMagnitude")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 ClampMagnitude")]
    public class Vector3ClampMagnitude : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 vector;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float maxlength;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            vector = GetInputValue(nameof(vector), vector);
            maxlength = GetInputValue(nameof(maxlength), maxlength);

            return Vector3.ClampMagnitude(vector, maxlength);
        } 
    }
}