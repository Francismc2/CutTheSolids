using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("AddForce")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody AddForce")]
    public class AddForce : AFXActiveNode
    {
        [SerializeField][NodeEnum] private ForceMode forceMode;
        [SerializeField][Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool worldSpace = false;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 forceIn;

        void ApplyForce()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            forceIn = GetInputValue(nameof(forceIn), forceIn);
            if (worldSpace)
            {
                rigidbodyIn.AddForce(forceIn, forceMode);
            }
            else
            {
                rigidbodyIn.AddRelativeForce(forceIn, forceMode);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ApplyForce();
            base.ExecuteNode(exit);
        }
    }
}