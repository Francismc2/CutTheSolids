using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("AddTorque")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody AddTorque")]
    public class AddTorque : AFXActiveNode
    {
        [SerializeField][NodeEnum] private ForceMode forceMode;
        [SerializeField][Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool worldSpace = false;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 forceIn;

        void ApplyTorque()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            forceIn = GetInputValue(nameof(forceIn), forceIn);
            if (worldSpace)
            {
                rigidbodyIn.AddTorque(forceIn, forceMode);
            }
            else
            {
                rigidbodyIn.AddRelativeTorque(forceIn, forceMode);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ApplyTorque();
            base.ExecuteNode(exit);
        }
    }
}