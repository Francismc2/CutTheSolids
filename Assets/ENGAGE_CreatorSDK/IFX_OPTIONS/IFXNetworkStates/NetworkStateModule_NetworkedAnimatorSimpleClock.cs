using UnityEngine;

namespace Engage.IFX.NetworkStates
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ENGAGE/Networked Animator (Simple Clock)")]
    public class NetworkStateModule_NetworkedAnimatorSimpleClock : NetworkStateModule
    {
        [Header("Make changes via this script to ensure sync. Call BecomeOwner() first if you need control.")]
        [SerializeField] private Animator animator;

        [Space(8)]
        [Header("Optional coarse drift correction against Photon clock.")]
        [SerializeField] private bool applyDriftCorrections = true;
        [SerializeField] private float driftCorrectionSeconds = 0.30f;

        private void Update()
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

        public void SetSpeed(float newSpeed)
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
