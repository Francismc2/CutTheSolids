using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set CenterOfMass")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set CenterOfMass")]
    public class SetCenterOfMass : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 centerOfMass;

        void SetCenterOfMassOnRB()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            centerOfMass = GetInputValue(nameof(centerOfMass), centerOfMass);
            rigidbody.centerOfMass = centerOfMass;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetCenterOfMassOnRB();
            base.ExecuteNode(exit);
        }
    }
}