using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("ToAngleAxis")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion ToAngleAxis")]
    public class QuaternionToAngleAxis : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternion;

        [SerializeField] [Output(ShowBackingValue.Never)] private float angle;
        [SerializeField] [Output(ShowBackingValue.Never)] private Vector3 axis;

        public override object GetValue(NodePort port)
        {
            quaternion = GetInputValue(nameof(quaternion), quaternion);
            quaternion.ToAngleAxis(out angle, out axis);

            if (port.fieldName == nameof(angle))
            {
                return angle;
            }

            if (port.fieldName == nameof(axis))
            {
                return axis;
            }

            return null;
        }
    }
}