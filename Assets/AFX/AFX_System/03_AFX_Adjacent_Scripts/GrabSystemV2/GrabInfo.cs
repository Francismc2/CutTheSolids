using UnityEngine;

namespace Engage.AFX.GrabSystem.v2
{
    public class GrabInfo
    {
        public bool CurrentlyGrabbed { get; set; }
        public HandType Hand { get; set; }
        public Pose? Offset { get; set; }
        public Transform Transform { get; set; }
        public Transform TargetTransform { get; set; }
    }
}