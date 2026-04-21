using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("AddForceAtPosition")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody AddForceAtPosition")]
    public class AddForceAtPosition : AFXActiveNode
    {
        [SerializeField][NodeEnum] private ForceMode forceMode;
        [SerializeField][Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 force;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 pos;

        void ApplyForce()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            force = GetInputValue(nameof(force), force);
            pos = GetInputValue(nameof(pos), pos);

            rigidbody.AddForceAtPosition(force, pos, forceMode);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ApplyForce();
            base.ExecuteNode(exit);
        }
    }
}