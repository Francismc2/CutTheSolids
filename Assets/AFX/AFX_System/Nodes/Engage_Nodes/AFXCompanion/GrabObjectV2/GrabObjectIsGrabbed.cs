using UnityEngine;
using XNode;
using Engage.AFX.v1;

namespace Engage.AFX.GrabSystem.v2
{
    [NodeTitle("GrabObject is grabbed")]
    [CreateNodeMenu(AFXMenuTree.GrabObject+ "GrabObject Is Grabbed")]
    public class GrabObjectIsGrabbed: AFXNode
    {
        [SerializeField] [Input] private GrabObject grabObjectIn;

        [SerializeField] [Output] private bool isGrabbed;
        [SerializeField] [Output] private bool isGrabbedLeftHand;
        [SerializeField] [Output] private bool isGrabbedRightHand;
        [SerializeField] [Output] private bool isGrabbedMouse;

        public override object GetValue(NodePort port)
        {
            return false;
        }
    }
}