using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GameObject Info")]
    [CreateNodeMenu(AFXMenuTree.GameObject+ "GameObject Info")]
    public class GameObjectInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private GameObject input;
        [SerializeField]
        [Output] private bool active;
        [SerializeField]
        [Output] private string goName;
        [SerializeField]
        [Output] private Transform transform;
        [SerializeField]
        [Output] private int layer;
        [SerializeField]
        [Output] private string tag;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            if (port.fieldName == nameof(active))
            {
                return input.activeSelf;
            }
            if (port.fieldName == nameof(goName))
            {
                return input.name;
            }
            if (port.fieldName == nameof(transform))
            {
                return input.transform;
            }
            if (port.fieldName == nameof(layer))
            {
                return input.layer;
            }
            if (port.fieldName == nameof(tag))
            {
                return input.tag;
            }
            return null;
        }
    }
}