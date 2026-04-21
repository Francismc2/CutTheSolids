using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Split Vector3")]
    [CreateNodeMenu(AFXMenuTree.Vector3 + "Split Vector3")]
    public class Vector3Splitter : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector3 vector3In;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private float x;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float y;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float z;

        public override object GetValue(NodePort port)
        {
            vector3In = GetInputValue(nameof(vector3In), vector3In);
            if (port.fieldName == nameof(x))
            {
                return vector3In.x;
            }
            if (port.fieldName == nameof(y))
            {
                return vector3In.y;
            }
            if (port.fieldName == nameof(z))
            {
                return vector3In.z;
            }
            return null;
        }
    }
}