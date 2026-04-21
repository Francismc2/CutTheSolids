using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Tolerance")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJointSpring + "Set SpringJoint Tolerance")]
    public class SetSpringJointTolerance : AFXActiveNode
    {
        [SerializeField]
        [Input] private SpringJoint input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float tolerance;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            tolerance = GetInputValue(nameof(tolerance), tolerance);
            input.tolerance = tolerance;
            base.ExecuteNode(exit);
        }
    }
}