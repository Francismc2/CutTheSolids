using UnityEngine;
using XNode;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [CreateNodeMenu(AFXMenuTree.TransformGet+ "Transform Get Position")]
    public class GetPosition : AFXNode
    {
        [SerializeField] [Input] private Transform transform;
        [SerializeField] [Output] private Vector3 output;
        
        public override object GetValue(NodePort port)
        {
            transform = GetInputValue(nameof(transform), transform);

            return transform.position;
        }
    }
}