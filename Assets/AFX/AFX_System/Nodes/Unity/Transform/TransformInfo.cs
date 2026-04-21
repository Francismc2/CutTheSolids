using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform Info")]
    public class TransformInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform input;
        [SerializeField]
        [Output] private GameObject gameObject;
        [SerializeField]
        [Output] private Transform parent;
        [SerializeField]
        [Output] private Vector3 right;
        [SerializeField]
        [Output] private Vector3 up;
        [SerializeField]
        [Output] private Vector3 forward;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            if (port.fieldName == nameof(gameObject))
            {
                return input.gameObject;
            }
            if (port.fieldName == nameof(parent))
            {
                return input.parent;
            }
            if (port.fieldName == nameof(right))
            {
                return input.right;
            }
            if (port.fieldName == nameof(up))
            {
                return input.up;
            }
            if (port.fieldName == nameof(forward))
            {
                return input.forward;
            }
            return null;
        }
    }
}