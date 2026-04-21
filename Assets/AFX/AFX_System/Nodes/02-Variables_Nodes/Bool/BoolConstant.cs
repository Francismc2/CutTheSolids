using XNode;
using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Bool + "Bool Constant")]
    public class BoolConstant : AFXNode
    {
        [SerializeField]
        [Output(ShowBackingValue.Always)] private bool output;

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}