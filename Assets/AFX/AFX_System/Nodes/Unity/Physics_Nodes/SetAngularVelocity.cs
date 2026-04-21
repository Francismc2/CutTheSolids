using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Set AngularVelocity")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set AngularVelocity")]
    public class SetAngularVelocity : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 angularVelocityIn;

        void SetAngularVelocityOnRB()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            angularVelocityIn = GetInputValue(nameof(angularVelocityIn), angularVelocityIn);
            rigidbodyIn.angularVelocity = angularVelocityIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetAngularVelocityOnRB();
            base.ExecuteNode(exit);
        }
    }
}