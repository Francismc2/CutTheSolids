using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("PingPong")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF PingPong")]
    public class PingPong : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float t;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float length;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            t = GetInputValue(nameof(t), t);
            length = GetInputValue(nameof(length), length);

            return Mathf.PingPong(t,length);
        }
    }
}