using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Get Vector3")]
    [CreateNodeMenu(AFXMenuTree.Vector3 + "Get Vector3")]
    public class GetVector3 : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector3Component vector3Component;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Vector3 vector3Out;

        public override object GetValue(NodePort port)
        {
            vector3Out = GetInputValue(nameof(vector3Component), vector3Component).Value;
            return vector3Out;
        }
    }
}