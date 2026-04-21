using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Int + "Get Int")]
    public class GetInt : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private IntComponent intComponent;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float output;

        public override object GetValue(NodePort port)
        {
            output = GetInputValue(nameof(intComponent), intComponent).Value;
            return output;
        }
    }
}
