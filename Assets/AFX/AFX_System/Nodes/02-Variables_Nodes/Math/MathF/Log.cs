using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Log")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Log")]
    public class Log : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float f;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float p;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            f = GetInputValue(nameof(f), f);
            p = GetInputValue<float>(nameof(p), p);

            return Mathf.Log(f, p);
        }
    }
}