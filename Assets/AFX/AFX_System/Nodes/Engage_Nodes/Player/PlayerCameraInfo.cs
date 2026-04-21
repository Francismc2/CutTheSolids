using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Player + "Player Camera Info")]
    public class PlayerCameraInfo : AFXNode, IDelayUntilReady
    {
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Camera playerCameraOut;
        [SerializeField]
        [Output] private Transform transformOut;

        public override object GetValue(NodePort port)
        {
            return null;
        }

        bool IDelayUntilReady.IsValueReady()
        {
            return true;
        }
    }
}