using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Pow")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Pow")]
    public class Pow : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float f;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float p;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            f = GetInputValue(nameof(f), f);
            p = GetInputValue<float>(nameof(p), p);

            return Mathf.Pow(f, p);
        }
    }
}