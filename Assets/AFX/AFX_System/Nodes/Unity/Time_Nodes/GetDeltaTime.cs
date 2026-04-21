using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Get DeltaTime")]
    [CreateNodeMenu(AFXMenuTree.Time + "Get DeltaTime")]
    public class GetDeltaTime : AFXNode
    {
        [SerializeField]
        [Output] private float deltaTime;

        public override object GetValue(NodePort port)
        {
            return Time.deltaTime;
        }
    }
}