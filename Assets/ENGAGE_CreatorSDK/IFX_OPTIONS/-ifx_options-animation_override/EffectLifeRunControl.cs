using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectLifeRunControl : MonoBehaviour {

    Animation anim; //Legacy

    Animator animator; //Modern


    AudioSource audioSource;

    Vector3 currentPos;
    Vector3 lastPos;
    public float animationSpeed = 1f;
    public string animationWalk;
    public string animationRun;
    public string animationIdle;
    public string animationRoar;
    public string[] animationsRoar;
    public float growlSeconds = 2.5f;
    public float[] growlsSeconds;
    public int roarOdds = 4;

    [Tooltip("Only used by Animator Objects.")]
    public float crossFadeTime = 0.3f;

    /*
     * New Component Parts
     */
    private bool m_isLegacy = false; //Will be used to check whether using legacy or new component
    AnimationClip[] m_clips;
    private List<string> m_clipNames = new List<string>();
    public AudioClip walkAudio;
    public AudioClip runAudio;
    public AudioClip roarAudio;
    public AudioClip idleAudio;
    public float audioVolume = 0.4f;
    bool growling = false;
    bool idle = false;
    bool running = false;
    bool walking = false;
    bool overriding = false;
    float timeStartedDelta;
    bool timeDeltaReceivedFromTrackScript = false;

}