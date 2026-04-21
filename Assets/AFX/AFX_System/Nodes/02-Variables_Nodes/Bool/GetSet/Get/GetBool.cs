using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Bool + "Get Bool")]
    public class GetBool : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private BoolComponent boolComponent;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private bool boolOut;

        public override object GetValue(NodePort port)
        {
            boolOut = GetInputValue(nameof(boolComponent), boolComponent).Value;
            return boolOut;
        }
    }
}
