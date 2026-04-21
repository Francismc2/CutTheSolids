using UnityEngine;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Reset To Rest")]
    public class GrabObjectResetToRest : MonoBehaviour
    {
        [SerializeField] private Transform restTransform;

        public void SetNewRestPoint(Transform newRestPoint)
        {
            restTransform = newRestPoint;
        }
    }
}