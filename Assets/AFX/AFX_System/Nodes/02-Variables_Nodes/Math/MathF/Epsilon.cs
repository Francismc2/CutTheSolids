using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Epsilon")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Epsilon")]
    public class Epsilon : AFXNode
    {
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            return Mathf.Epsilon;
        }
    }
}