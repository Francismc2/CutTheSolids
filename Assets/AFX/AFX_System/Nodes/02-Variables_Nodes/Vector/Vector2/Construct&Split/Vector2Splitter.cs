using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Split Vector2")]
    [CreateNodeMenu(AFXMenuTree.Vector2 + "Split Vector2")]
    public class Vector2Splitter : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector2 vector2In;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private float x;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float y;

        public override object GetValue(NodePort port)
        {
            vector2In = GetInputValue(nameof(vector2In), vector2In);
            if (port.fieldName == nameof(x))
            {
                return vector2In.x;
            }
            if (port.fieldName == nameof(y))
            {
                return vector2In.y;
            }
            return null;
        }
    }
}