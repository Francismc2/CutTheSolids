using XNode;
using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Constant")]
    public class StringConstant : AFXNode
    {
        [TextArea(10, 50)]
        [SerializeField]
        [Output(ShowBackingValue.Always)] private string output;

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}