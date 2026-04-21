using UnityEngine;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set Kinematic")]
    public class SetKinematic : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool kinematic;

        void SetKinematicSettings()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            kinematic = GetInputValue(nameof(kinematic), kinematic);

            rigidbody.isKinematic = kinematic;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetKinematicSettings();
            base.ExecuteNode(exit);
        }
    }
}