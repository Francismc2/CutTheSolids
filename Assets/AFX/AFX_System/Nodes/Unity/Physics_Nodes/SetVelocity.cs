using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set Velocity")]
    public class SetVelocity : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 velocityIn;

        

        void SetVelocityOnRB()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            velocityIn = GetInputValue(nameof(velocityIn), velocityIn);

            rigidbodyIn.velocity = velocityIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetVelocityOnRB();
            base.ExecuteNode(exit);
        }
    }
}