using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set AngularDrag")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set AngularDrag")]
    public class SetAngularDrag : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float angularDrag;

        void SetAngularDragOnRB()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            angularDrag = GetInputValue(nameof(angularDrag), angularDrag);
            rigidbody.angularDrag = angularDrag;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetAngularDragOnRB();
            base.ExecuteNode(exit);
        }
    }
}