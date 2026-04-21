using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Spring")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJointSpring + "Set SpringJoint Spring")]
    public class SetSpringJointSpring : AFXActiveNode
    {
        [SerializeField]
        [Input] private SpringJoint input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float spring;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            spring = GetInputValue(nameof(spring), spring);
            input.spring = spring;
            base.ExecuteNode(exit);
        }
    }
}