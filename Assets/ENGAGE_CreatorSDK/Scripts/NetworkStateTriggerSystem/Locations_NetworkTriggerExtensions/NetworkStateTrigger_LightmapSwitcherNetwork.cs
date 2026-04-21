using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSS;


[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_LightmapSwitcherNetwork : MonoBehaviour
{
    private LVR_Location_NetworkState m_state;

    [Header("Reference to LSS_FrontEnd object")]
    public LSS_FrontEnd lightSwitcher;

    [Header("Array containing objects to enable during lightmodes")]
    public GameObject[] lightObjects;

    [Header("String to represent Lightmaps")]
    public string[] lightmapNames;

    private int lastLight;

    void Start()
    {
        m_state = GetComponent<LVR_Location_NetworkState>();
        lastLight = m_state.currentState;
    }

    public void SetState(int state)
    {
        m_state.UpdateState(state);
    }

    public void SetStateDelay(int state)
    {
        StartCoroutine(WaitTimeStateChange(state));
    }

    public IEnumerator WaitTimeStateChange(int state)
    {
        yield return new WaitForSeconds(5);
        m_state.UpdateState(state);
    }

    public void CycleState()
    {
        if (m_state.currentState < lightmapNames.Length - 1)
        {
            m_state.UpdateState(m_state.currentState + 1);
        }
        else
        {
            m_state.UpdateState(0);
        }
    }

    private void EnableLights(int lightMode)
    {
        for (int i = 0; i < lightObjects.Length; i++)
        {
            if (i == lightMode)
            {
                lightObjects[i].SetActive(true);
            }
            else
            {
                lightObjects[i].SetActive(false);
            }
        }
    }

    private void ChangeLightmaps(int lightMode)
    {
        lightSwitcher.LoadFromSO(lightMode);
        EnableLights(lightMode);
    }

    private void FixedUpdate()
    {
        if (m_state.currentState != lastLight)
        {
            lastLight = m_state.currentState;
            ChangeLightmaps(m_state.currentState);
        }
    }
}
