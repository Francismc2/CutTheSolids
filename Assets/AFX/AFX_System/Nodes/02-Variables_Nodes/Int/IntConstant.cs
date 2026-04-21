using XNode;
using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Int + "Int Constant")]
    public class IntConstant : AFXNode
    {
        [SerializeField]
        [Output(ShowBackingValue.Always)] private int output;

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}