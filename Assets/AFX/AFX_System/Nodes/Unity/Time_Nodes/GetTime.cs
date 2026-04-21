using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Time + "Get Time")]
    public class GetTime : AFXNode
    {
        [SerializeField]
        [Output] private float time;

        public override object GetValue(NodePort port)
        {
            return Time.time;
        }
    }
}