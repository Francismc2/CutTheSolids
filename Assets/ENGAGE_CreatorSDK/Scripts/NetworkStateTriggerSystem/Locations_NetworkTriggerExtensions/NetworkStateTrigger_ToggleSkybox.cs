using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Toggle Skybox in Scene
/// </summary>

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_ToggleSkybox : MonoBehaviour {

    private LVR_Location_NetworkState m_state;

    [Header("Skybox material to toggle with current skybox")]
    public Material skybox;

    private Material m_defaultSkybox;

    void Start () {
        m_state = GetComponent<LVR_Location_NetworkState>();
        m_defaultSkybox = RenderSettings.skybox;
    }

    public void ToggleState()
    {
        if (m_state.currentState == 0)
        {
            m_state.UpdateState(1);
        }

        else
        {
            m_state.UpdateState(0);
        }
    }

    public void SetState(int state)
    {
        m_state.UpdateState(state);
    }

    void ObjectUpdate()
    {
      if (m_state.currentState == 0)
        {
            RenderSettings.skybox = m_defaultSkybox;
        }
        else
        {
            RenderSettings.skybox = skybox;
        }
    }

    private void Update()
    {
        ObjectUpdate();
    }
}
