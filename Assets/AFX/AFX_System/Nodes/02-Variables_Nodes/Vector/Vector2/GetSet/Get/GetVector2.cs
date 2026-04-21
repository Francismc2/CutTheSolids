using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Vector2 + "Get Vector2")]
    public class GetVector2 : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector2Component vector2Component;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Vector2 vector2Out;

        public override object GetValue(NodePort port)
        {
            vector2Out = GetInputValue(nameof(vector2Component), vector2Component).Value;
            return vector2Out;
        }
    }
}
