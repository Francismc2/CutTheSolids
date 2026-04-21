using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set MinDistance")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJointSpring + "Set SpringJoint MinDistance")]
    public class SetSpringJointMinDistance : AFXActiveNode
    {
        [SerializeField]
        [Input] private SpringJoint input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float minDistance;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            minDistance = GetInputValue(nameof(minDistance), minDistance);
            input.minDistance = minDistance;
            base.ExecuteNode(exit);
        }
    }
}