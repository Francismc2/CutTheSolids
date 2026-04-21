using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set SetGravity")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set UseGravity")]
    public class SetUseGravity : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool useGravity;

        void SetUseGravityOnRB()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            useGravity = GetInputValue(nameof(useGravity), useGravity);
            rigidbody.useGravity = useGravity;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetUseGravityOnRB();
            base.ExecuteNode(exit);
        }
    }
}