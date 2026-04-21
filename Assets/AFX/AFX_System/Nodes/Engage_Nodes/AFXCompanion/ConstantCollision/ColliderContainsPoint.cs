using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ConstantCollision+ "Collider Contains Point")]
    public class ColliderContainsPoint : AFXNode
    {
        [SerializeField]
        [Input] private Collider collider;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 point;

        [SerializeField]
        [Output] private bool output;

        public override object GetValue(NodePort port)
        {
            collider = GetInputValue(nameof(collider), collider);
            point = GetInputValue(nameof(point), point);
            return AFXColliders.ColliderContainsPoint(collider, point);
        }       
    }
}
