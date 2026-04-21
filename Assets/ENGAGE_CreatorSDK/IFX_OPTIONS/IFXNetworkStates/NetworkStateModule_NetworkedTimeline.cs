using UnityEngine;
using UnityEngine.Playables;

namespace Engage.IFX.NetworkStates
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ENGAGE/Networked Timeline (Simple Clock)")]
    public class NetworkStateModule_NetworkedTimeline : NetworkStateModule
    {
        [Header("Make changes via this script. Call BecomeOwner() first if you need control.")]
        [SerializeField] private PlayableDirector director;

        private void Awake()
        {
        }

        public void Update()
        {
        }

        public void BecomeOwner()
        {
        }

        public void Play()
        {
        }

        public void Pause()
        {
        }

        public void SetTimeSeconds(float seconds)
        {
        }

        public void SetTimeNormalized(float normalized)
        {
        }

        public void Restart()
        {
        }
    }
}