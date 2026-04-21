using UnityEngine;
using XNode;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [NodeTitle("Get LocalPosition")]
    [CreateNodeMenu(AFXMenuTree.TransformGet+ "Transform Get LocalPosition")]
    public class GetLocalPosition : AFXNode
    {
        [SerializeField] [Input] private Transform transform;
        [SerializeField] [Output] private Vector3 localPosition;
        
        public override object GetValue(NodePort port)
        {
            transform = GetInputValue(nameof(transform), transform);

            return transform.localPosition;
        }
    }
}