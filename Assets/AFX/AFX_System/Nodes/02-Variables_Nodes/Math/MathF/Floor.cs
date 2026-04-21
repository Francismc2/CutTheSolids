using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Floor")]
    [CreateNodeMenu(AFXMenuTree.MathF + "Mathf Floor")]
    public class Floor : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return Mathf.Floor(input);
        }
    }
}