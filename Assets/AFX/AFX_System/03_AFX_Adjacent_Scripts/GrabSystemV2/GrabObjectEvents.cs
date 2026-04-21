using UnityEngine;
using UnityEngine.Events;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Events")]
    public class GrabObjectEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent onGrab;
        [SerializeField] private UnityEvent onGrabStay;
        [SerializeField] private UnityEvent onGrabRelease;
    }
}