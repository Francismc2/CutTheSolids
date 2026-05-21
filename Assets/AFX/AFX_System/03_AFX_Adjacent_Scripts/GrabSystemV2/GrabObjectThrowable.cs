using UnityEngine;
using Engage.AFX.v1;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Throwable")]
    public class GrabObjectThrowable : MonoBehaviour
    {
        [SerializeField] private float throwStrength = 1;
        [SerializeField] private bool useObjectMass = false;

        private void OnEnable()
        {
        }
    }
}