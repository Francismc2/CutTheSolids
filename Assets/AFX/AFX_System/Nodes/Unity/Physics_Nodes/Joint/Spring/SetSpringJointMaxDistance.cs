using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set MaxDistance")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJointSpring + "Set SpringJoint MaxDistance")]
    public class SetSpringJointMaxDistance : AFXActiveNode
    {
        [SerializeField]
        [Input] private SpringJoint input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float maxDistance;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            maxDistance = GetInputValue(nameof(maxDistance), maxDistance);
            input.maxDistance = maxDistance;
            base.ExecuteNode(exit);
        }
    }
}