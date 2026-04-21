using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("NegativeInfinity")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF NegativeInfinity")]
    public class NegativeInfinity : AFXNode
    {
        [SerializeField][Output] private float infinity;

        public override object GetValue(NodePort port)
        {
            return Mathf.NegativeInfinity;
        }
    }
}