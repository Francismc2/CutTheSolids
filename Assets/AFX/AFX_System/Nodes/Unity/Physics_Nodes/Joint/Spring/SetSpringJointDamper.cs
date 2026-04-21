using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Damper")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJointSpring + "Set SpringJoint Damper")]
    public class SetSpringJointDamper : AFXActiveNode
    {
        [SerializeField]
        [Input] private SpringJoint input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float damper;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            damper = GetInputValue(nameof(damper), damper);
            input.damper = damper;
            base.ExecuteNode(exit);
        }
    }
}