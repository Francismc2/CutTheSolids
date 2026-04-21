using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Deg2Rad")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Deg2Rad")]
    public class Deg2Rad : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float deg;

        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            deg = GetInputValue(nameof(deg), deg);

            return deg * Mathf.Deg2Rad;
        }
    }
}