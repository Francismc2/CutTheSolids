using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Asin")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Asin")]
    public class Asin : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.Asin(input);
        }
    }
}