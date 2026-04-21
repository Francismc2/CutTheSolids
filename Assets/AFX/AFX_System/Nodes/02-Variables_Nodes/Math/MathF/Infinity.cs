using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Infinity")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Infinity")]
    public class Infinity : AFXNode
    {
        [SerializeField][Output] private float infinity;

        public override object GetValue(NodePort port)
        {
            return Mathf.Infinity;
        }
    }
}