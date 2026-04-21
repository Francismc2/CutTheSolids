using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set MotorTorque")]
    [CreateNodeMenu(AFXMenuTree.WheelCollider + "WheelCollider Set MotorTorque")]
    public class WheelColliderSetMotorTorque : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private WheelCollider wheelCollider;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float motorTorque;

        private void ApplyWheelSettings()
        {
            wheelCollider = GetInputValue(nameof(wheelCollider), wheelCollider);
            motorTorque = GetInputValue(nameof(motorTorque), motorTorque);

            wheelCollider.motorTorque = motorTorque;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ApplyWheelSettings();
            base.ExecuteNode(exit);
        }
    }
}