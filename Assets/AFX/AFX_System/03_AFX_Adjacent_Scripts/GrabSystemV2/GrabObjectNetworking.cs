using Engage.IFX.NetworkStates;
using UnityEngine;

namespace Engage.AFX.GrabSystem.v2
{
    [RequireComponent(typeof(GrabObject))]
    [AddComponentMenu("AFX/Interaction/GrabObject/GrabObject - Networking")]
    public class GrabObjectNetworking : NetworkStateModule
    {
        [SerializeField] private float interpolateSpeed = 1f;
    }
}