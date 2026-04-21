using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Atan2")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Atan2")]
    public class Atan2 : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input1;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input2;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            input1 = GetInputValue(nameof(input1), input1);
            input2 = GetInputValue(nameof(input2), input2);

            return Mathf.Atan2(input1,input2);
        }
    }
}