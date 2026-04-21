using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set BrakeTorque")]
    [CreateNodeMenu(AFXMenuTree.WheelCollider + "WheelCollider Set BrakeTorque")]
    public class WheelColliderSetBrakeTorque : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private WheelCollider wheelCollider;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float brakeTorque;

        private void ApplyWheelSettings()
        {
            wheelCollider = GetInputValue(nameof(wheelCollider), wheelCollider);
            brakeTorque = GetInputValue(nameof(brakeTorque), brakeTorque);

            wheelCollider.brakeTorque = brakeTorque;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ApplyWheelSettings();
            base.ExecuteNode(exit);
        }
    }
}