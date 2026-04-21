using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set MovePosition")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody MovePosition")]
    public class MovePosition : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 vector3In;

        

        void MoveRBPosition()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            vector3In = GetInputValue(nameof(vector3In), vector3In);

            rigidbodyIn.MovePosition(vector3In);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            MoveRBPosition();
            base.ExecuteNode(exit);
        }
    }
}