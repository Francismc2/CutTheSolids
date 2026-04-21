using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Get FixedTime")]
    [CreateNodeMenu(AFXMenuTree.Time + "Get FixedTime")]
    public class GetFixedTime : AFXNode
    {
        [SerializeField]
        [Output] private float fixedTime;

        public override object GetValue(NodePort port)
        {
            return Time.fixedTime;
        }
    }
}