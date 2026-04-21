using Engage.AFX.v1;
using System.Collections.Generic;
using UnityEngine;

namespace Engage.AFX.GrabSystem.v2
{
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Main")]
    public class GrabObject : MonoBehaviour
    {
        [SerializeField] private Collider colliderToGrab;
        [SerializeField] private bool maintainOffset;
        [SerializeField] private bool requireTriggerPress;
        [SerializeField] private bool requireGrippedPress;
        [SerializeField] private float maxGrabDistance = 5f;
        [SerializeField] private bool allowVRGrabAtRange;

        //Grabs will redirect to networking module if there is one.
        public delegate void GrabDelegate(GrabInfo grabInfo);
        private GrabDelegate localOnGrab;
        private GrabDelegate localOnGrabStay;
        private GrabDelegate localOnGrabRelease;
    }
}