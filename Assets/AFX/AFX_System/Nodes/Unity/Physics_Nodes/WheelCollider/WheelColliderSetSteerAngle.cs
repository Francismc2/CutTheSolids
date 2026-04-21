using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set SteerAngle")]
    [CreateNodeMenu(AFXMenuTree.WheelCollider + "WheelCollider Set SteerAngle")]
    public class WheelColliderSetSteerAngle : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private WheelCollider wheelCollider;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float steerAngle;

        private void ApplyWheelSettings()
        {
            wheelCollider = GetInputValue(nameof(wheelCollider), wheelCollider);
            steerAngle = GetInputValue(nameof(steerAngle), steerAngle);

            wheelCollider.steerAngle = steerAngle;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ApplyWheelSettings();
            base.ExecuteNode(exit);
        }
    }
}