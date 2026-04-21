using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Log10")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Log10")]
    public class Log10 : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float f;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            f = GetInputValue(nameof(f), f);

            return Mathf.Log10(f);
        }
    }
}