using UnityEngine;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Mover")]
    public class GrabObjectMover : MonoBehaviour
    {
        [SerializeField] private bool movable = true;
        [SerializeField] private bool rotatable = true;
    }
}