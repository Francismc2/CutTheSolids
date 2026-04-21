using UnityEngine;
using XNode;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [NodeTitle("Get LossyScale")]
    [CreateNodeMenu(AFXMenuTree.TransformGet + "Transform Get LossyScale")]
    public class GetLossyScale : AFXNode
    {
        [SerializeField] [Input] private Transform transform;

        [SerializeField] [Output] private Vector3 lossyScale;
        
        public override object GetValue(NodePort port)
        {
            transform = GetInputValue(nameof(transform), transform);

            return transform.lossyScale;
        }
    }
}