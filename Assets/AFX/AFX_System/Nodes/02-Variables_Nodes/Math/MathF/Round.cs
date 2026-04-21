using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Round")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Round")]
    public class Round : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float f;

        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            f = GetInputValue(nameof(f), f);

            return Mathf.Round(f);
        }
    }
}