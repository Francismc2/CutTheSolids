using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("MoveRotation")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody MoveRotation")]
    public class MoveRotation : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Quaternion quaternionIn;

        

        void MoveRBRotation()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            quaternionIn = GetInputValue(nameof(quaternionIn), quaternionIn);

            rigidbodyIn.MoveRotation(quaternionIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            MoveRBRotation();
            base.ExecuteNode(exit);
        }
    }
}