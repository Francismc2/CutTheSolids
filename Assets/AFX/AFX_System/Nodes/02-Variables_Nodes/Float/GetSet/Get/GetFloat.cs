using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Float + "Get Float")]
    public class GetFloat : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private FloatComponent floatComponent;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float output;


        public override object GetValue(NodePort port)
        {
            output = GetInputValue(nameof(floatComponent),floatComponent).Value;
            return output;
        }
    }
}
