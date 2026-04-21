using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set Mass")]
    public class SetMass : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float mass;

        void SetMassOnRB()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            mass = GetInputValue(nameof(mass), mass);
            rigidbody.mass = mass;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetMassOnRB();
            base.ExecuteNode(exit);
        }
    }
}