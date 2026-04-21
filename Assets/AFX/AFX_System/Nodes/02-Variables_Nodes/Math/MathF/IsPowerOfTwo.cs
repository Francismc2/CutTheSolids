using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("IsPowerOfTwo")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF IsPowerOfTwo")]
    public class IsPowerOfTwo : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int value;
        
        [SerializeField][Output] private bool output;

        public override object GetValue(NodePort port)
        {
            value = GetInputValue(nameof(value), value);;

            return Mathf.IsPowerOfTwo(value);
        }
    }
}