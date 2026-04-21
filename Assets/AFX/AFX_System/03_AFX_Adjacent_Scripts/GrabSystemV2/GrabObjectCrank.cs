using UnityEngine;
using Engage.AFX.v1;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Crank")]
    public class GrabObjectCrank : MonoBehaviour
    {
        [Header("-Axis To Rotate Around-")]
        [SerializeField] private bool x;
        [SerializeField] private bool y;
        [SerializeField] private bool z;

        private GrabObject grabObject;
        private Vector3? lastCrankAngle;
        private Vector3 colliderCenter;
    }
}