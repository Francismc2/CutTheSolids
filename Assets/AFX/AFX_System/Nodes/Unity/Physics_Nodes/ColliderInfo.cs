using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentCollider + "Collider Info")]
    public class ColliderInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Collider input;
        [SerializeField]
        [Output] private bool enabled;
        [SerializeField]
        [Output] private bool isTrigger;
        [SerializeField]
        [Output] private string tag;
        [SerializeField]
        [Output] private GameObject gameObject;
        [SerializeField]
        [Output] private Transform transform;
        [SerializeField]
        [Output] private Rigidbody attachedRigidbody;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            if (port.fieldName == nameof(enabled))
            {
                return input.enabled;
            }
            if (port.fieldName == nameof(isTrigger))
            {
                return input.isTrigger;
            }
            if (port.fieldName == nameof(tag))
            {
                return input.tag;
            }
            if (port.fieldName == nameof(gameObject))
            {
                return input.gameObject;
            }            
            if (port.fieldName == nameof(transform))
            {
                return input.transform;
            }
            if (port.fieldName == nameof(attachedRigidbody))
            {
                return input.attachedRigidbody;
            }
            return null;
        }
    }
}