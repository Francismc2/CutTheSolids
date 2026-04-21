using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LVR_Effect4D_Special_FireTrigger : MonoBehaviour
{

    enum FireState
    {
        FullFire = 0,
        MiddlingFire = 1,
        HalfFire = 2,
        DyingFire = 3,
        DeadFire = 4
    }

    public int startState = 0;
    public int currentState = 0;
    public float extinguishTime = 2.5f;
    public float reigniteTime = 2.5f;

    private float _killTime = 0;
    private float _increaseTime = 0;

    public LVR_Effect4D_Trigger_Special_FireExtinguishSprayTrigger.ExtinguisherType fireExtinguishType;

    public UnityEvent onFireExtinguishedEvent;
    [System.Serializable]
    public class ParticleSystemEngageEffect
    {
        public ParticleSystem particleSystem;
        public float defaultStartLifetime;

        public ParticleSystemEngageEffect
            (
            ParticleSystem particleSystem,
            float defaultStartLifetime
            )
        {
            this.particleSystem = particleSystem;
            this.defaultStartLifetime = defaultStartLifetime;
        }
    }

    public ParticleSystem[] fireParticleSystems;
    public ParticleSystem[] smokeParticleSystems;

    public List<ParticleSystemEngageEffect> _fireParticleSystemEffects = new List<ParticleSystemEngageEffect>();
    public List<ParticleSystemEngageEffect> _smokeParticlesSystemEffects = new List<ParticleSystemEngageEffect>();

    float _triggerEnteredTimer = 0;
    LVR_Effect4D_Trigger_Special_FireExtinguishSprayTrigger.ExtinguisherType triggeredType;


}
