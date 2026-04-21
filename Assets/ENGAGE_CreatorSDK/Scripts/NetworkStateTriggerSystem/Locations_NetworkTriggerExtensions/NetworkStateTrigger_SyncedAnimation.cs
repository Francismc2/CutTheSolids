using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_SyncedAnimation : MonoBehaviour
{

    /*The Location Timer Value*/
    private LVR_Location_NetworkState m_state;

    /*Whether playing or not playing*/
    [Tooltip("State Value of 1 will Start Animation on Scene Start")]
    public LVR_Location_NetworkState playControllerState;

    /*Used to store what time the state was paused*/
    [Tooltip("Ensure State Value is set to 0")]
    public LVR_Location_NetworkState pauseTimeValueState;

    private Animator m_animator;


    void Start()
    {

    }

    public void Play()
    {

    }

    public void Pause()
    {

    }

    public void PlayFromBeginning()
    {

    }

    private void Update()
    {

    }

}
