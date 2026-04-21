using UnityEngine;
using XNode;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [NodeTitle("Get LocalScale")]
    [CreateNodeMenu(AFXMenuTree.TransformGet + "Transform Get LocalScale")]
    public class GetLocalScale : AFXNode
    {
        [SerializeField] [Input] private Transform transform;

        [SerializeField] [Output] private Vector3 localScale;
        
        public override object GetValue(NodePort port)
        {
            transform = GetInputValue(nameof(transform), transform);

            return transform.localScale;
        }
    }
}