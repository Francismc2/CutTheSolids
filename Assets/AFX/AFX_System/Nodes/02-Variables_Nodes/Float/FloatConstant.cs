using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Float + "Float Constant")]
    public class FloatConstant : AFXNode
    {
        [SerializeField]
        [Output(ShowBackingValue.Always)] private float output;

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}