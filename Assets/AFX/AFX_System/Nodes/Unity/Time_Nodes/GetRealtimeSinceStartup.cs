using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Get RealtimeSinceStartup")]
    [CreateNodeMenu(AFXMenuTree.Time + "Get RealtimeSinceStartup")]
    public class GetRealtimeSinceStartup : AFXNode
    {
        [SerializeField]
        [Output] private float realTimeSinceStartup;

        public override object GetValue(NodePort port)
        {
            return Time.realtimeSinceStartup;
        }
    }
}