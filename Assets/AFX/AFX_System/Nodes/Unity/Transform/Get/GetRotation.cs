using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.TransformGet + "Transform Get Rotation")]
    public class GetRotation : AFXNode
    {        
        [SerializeField]
        [Input] private Transform transformIn;

        [SerializeField]
        [Output] private Quaternion rotationOut;

        public override object GetValue(NodePort port)
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            return transformIn.rotation;
        }
    }
}