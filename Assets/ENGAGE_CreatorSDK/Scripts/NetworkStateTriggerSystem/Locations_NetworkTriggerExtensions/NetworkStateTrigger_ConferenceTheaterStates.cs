using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_ConferenceTheaterStates : MonoBehaviour
{
    private LVR_Location_NetworkState m_state;
    [Header("Script used exclusively for Theatre Stage Scene")]

    //[Tooltip("0=BasteState, 1=TheaterState, 2= PodiumState, 3=CinemaState")]
    [Tooltip("0=Curtains, 1= LargeScreenInFront, 2=ScreenBehind, 3=Podium, 4=SmallScreenInFront")]
    public List<NetworkStateTrigger_GenericMovable> controlledObjects;

    //[Tooltip("LightmapObject")]
    //public NetworkStateTrigger_Lightmap lightmapState;

    enum SceneState {BaseState, TheaterState, PodiumState, CinemaState};

    void Start()
    {
        m_state = GetComponent<LVR_Location_NetworkState>();
    }

    public void SetState(int state)
    {
        m_state.UpdateState(state);
    }

    public void ToggleScreen()
    {
       
        switch (m_state.currentState)
        {
            case (0)://(int)SceneState.BaseState):
                controlledObjects[2].ToggleState();
                break;
            case (1)://(int)SceneState.TheaterState):
                controlledObjects[2].ToggleState();
                break;
            case (2):// (int)SceneState.PodiumState):
                controlledObjects[4].ToggleState();
                break;
            case (3):// (int)SceneState.CinemaState):
                controlledObjects[1].ToggleState();
                break;
            default:
                Debug.Log("Current State not recognised by TheaterStates");
                break;
        }
    }
}
