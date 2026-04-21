using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("InversTransformVector")]
    [CreateNodeMenu(AFXMenuTree.TransformSpaceSwitching + "InversTransformVector")]
    public class InverseTransformVector : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Vector3 vector3In;

        [SerializeField]
        [Output] private Vector3 vector3Out;

        public override object GetValue(NodePort port)
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            vector3In = GetInputValue(nameof(vector3In), vector3In);
            return transformIn.InverseTransformVector(vector3In);
        }
    }
}