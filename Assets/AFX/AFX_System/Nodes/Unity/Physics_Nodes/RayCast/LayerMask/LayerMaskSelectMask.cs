using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Select LayerMask")]
    [CreateNodeMenu(AFXMenuTree.LayerMask + "LayerMask Select Mask")]
    public class LayerMaskSelectMask : AFXNode
    {
        [SerializeField] private LayerMask mask;
        [SerializeField][Output(ShowBackingValue.Never)] private int output;

        public override object GetValue(NodePort port)
        {
            return mask.value;
        }
    }
}