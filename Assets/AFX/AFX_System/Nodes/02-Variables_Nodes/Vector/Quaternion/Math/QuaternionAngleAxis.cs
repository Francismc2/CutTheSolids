using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("AngleAxis")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion AngleAxis")]
    public class QuaternionAngleAxis : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float angle;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 axis;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternion;

        public override object GetValue(NodePort port)
        {
            angle = GetInputValue(nameof(angle), angle);
            axis = GetInputValue(nameof(axis), axis);

            quaternion = Quaternion.AngleAxis(angle, axis);
            return quaternion;
        }
    }
}