using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Exp")]
    [CreateNodeMenu(AFXMenuTree.MathF + "Mathf Exp")]
    public class Exp : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float power;
        
        [SerializeField][Output] private float output;

        public override object GetValue(NodePort port)
        {
            power = GetInputValue(nameof(power), power);

            return Mathf.Exp(power);
        }
    }
}