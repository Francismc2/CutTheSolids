using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("PI")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF PI")]
    public class PI : AFXNode
    {
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            return Mathf.PI;
        }
    }
}