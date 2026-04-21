using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Repeat")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Repeat")]
    public class Repeat : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float f;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float length;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            f = GetInputValue(nameof(f), f);
            length = GetInputValue<float>(nameof(length), length);

            return Mathf.Repeat(f, length);
        }
    }
}