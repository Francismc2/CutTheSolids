using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Set Freeze")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set Freeze")]
    public class SetRigidbodyFreeze : AFXActiveNode
    {
        // Values copied from "RigidbodyConstraints" then bitshifted. Done this way to allow use of "[System.Flags]"
        [System.Flags]
        private enum AFXRigidbodyConstraints
        {
            None = 0,
            FreezeAll = ~0,

            FreezePositionX = 1 << 1,
            FreezePositionY = 1 << 2,
            FreezePositionZ = 1 << 3,
            FreezePosition = FreezePositionX | FreezePositionY | FreezePositionZ,

            FreezeRotationX = 1 << 4,
            FreezeRotationY = 1 << 5,
            FreezeRotationZ = 1 << 6,
            FreezeRotation = FreezeRotationX | FreezeRotationY | FreezeRotationZ,
        }

        [SerializeField] [Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;
        [SerializeField] [NodeEnum] private AFXRigidbodyConstraints rigidbodyConstraints;

        private void Freeze()
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            rigidbodyIn.constraints = (RigidbodyConstraints)rigidbodyConstraints;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            Freeze();
            base.ExecuteNode(exit);
        }
    }
}