using System.Collections.Generic;
using UnityEngine;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Exclude")]
    public class GrabObjectExclude : MonoBehaviour
    {
        [Header("GrabObjects in this list can't be grabbed while this is grabbed")]

        [SerializeField] private List<GrabObject> excludedGrabObjects;

        public List<GrabObject> ExcludedGrabObjects { get => excludedGrabObjects; }
    }
}