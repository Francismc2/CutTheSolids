using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.BoolLogic + "Bool And")]
    public class LogicBoolAnd : AFXNode
    {
        [SerializeField]
        [Input] private bool a;
        [SerializeField]
        [Input] private bool b;

        [SerializeField]
        [Output] private bool output;

        public override object GetValue(NodePort port)
        {
            a = GetInputValue(nameof(a), a);
            b = GetInputValue(nameof(b), b);

            return a && b;
        }
    }
}