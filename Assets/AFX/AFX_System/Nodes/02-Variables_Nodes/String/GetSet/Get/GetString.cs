using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "Get String")]
    public class GetString : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private StringComponent stringComponent;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private string output;

        public override object GetValue(NodePort port)
        {
            output = GetInputValue(nameof(stringComponent), stringComponent).Value;
            return output;
        }
    }
}