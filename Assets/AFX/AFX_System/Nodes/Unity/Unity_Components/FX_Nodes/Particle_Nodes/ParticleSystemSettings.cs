using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentFXParticle + "Particle System Settings")]
    public class ParticleSystemSettings : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private ParticleSystem particleSystemIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float rateOverTimeIn;

        public void ParticleUpdate()
        {
            particleSystemIn = GetInputValue(nameof(particleSystemIn), particleSystemIn);
            rateOverTimeIn = GetInputValue(nameof(rateOverTimeIn), rateOverTimeIn);
            var emission = particleSystemIn.emission;
            emission.rateOverTime = rateOverTimeIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ParticleUpdate();
        }
    }
}