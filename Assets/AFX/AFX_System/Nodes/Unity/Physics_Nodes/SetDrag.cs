using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Set Drag")]
    public class SetDrag : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbody;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float drag;

        void SetDragOnRB()
        {
            rigidbody = GetInputValue(nameof(rigidbody), rigidbody);
            drag = GetInputValue(nameof(drag), drag);
            rigidbody.drag = drag;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetDragOnRB();
            base.ExecuteNode(exit);
        }
    }
}