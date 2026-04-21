using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("Angle")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Angle")]
    public class QuaternionAngle : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternionA;
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion quaternionB;

        [SerializeField] [Output(ShowBackingValue.Never)] private float angle;

        public override object GetValue(NodePort port)
        {
            quaternionA = GetInputValue(nameof(quaternionA), quaternionA);
            quaternionB = GetInputValue(nameof(quaternionB), quaternionB);

            angle = Quaternion.Angle(quaternionA, quaternionB);
            return angle;
        }
    }
}