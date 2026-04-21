using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("HingeJoint Motor")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJointHinge + "HingeJoint Motor")]
    public class HingeJointMotor : AFXActiveNode
    {
        [SerializeField]
        [Input] private HingeJoint hinge;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float force;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float targetVelocity;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool freeSpin;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            hinge = GetInputValue(nameof(hinge), hinge);
            force = GetInputValue(nameof(force), force);
            targetVelocity = GetInputValue(nameof(targetVelocity), targetVelocity);
            freeSpin = GetInputValue(nameof(freeSpin), freeSpin);

            JointMotor motor = hinge.motor;
            motor.force = force;
            motor.targetVelocity = targetVelocity;
            motor.freeSpin = freeSpin;
            hinge.motor = motor;

            base.ExecuteNode(exit);
        }
    }
}