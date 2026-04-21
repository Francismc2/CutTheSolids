using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_UHDVideoDefault : MonoBehaviour {

    private LVR_Location_NetworkState m_state;

    public List<GameObject> toggledOnObjects = new List<GameObject>();
    public List<GameObject> toggledOffObjects = new List<GameObject>();

    void Start () {
        m_state = GetComponent<LVR_Location_NetworkState>();
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

    float updateRate = 0.5f;
    float updateCounter = 0;
    void ObjectUpdate()
    {
        if (updateCounter < updateRate)
        {
            updateCounter += Time.deltaTime;
            return;
        }

        updateCounter = 0;

        for(int i=0;i<toggledOnObjects.Count;i++)
        {
            if (toggledOnObjects[i] != null && toggledOnObjects[i].activeSelf != (m_state.currentState > 0))
            {
                toggledOnObjects[i].SetActive(m_state.currentState > 0);
            }
        }
        for (int i = 0; i < toggledOffObjects.Count; i++)
        {
            if (toggledOffObjects[i] != null && toggledOffObjects[i].activeSelf != (m_state.currentState <= 0))
            {
                toggledOffObjects[i].SetActive(m_state.currentState <= 0);
            }
        }
    }

    private void Update()
    {
        ObjectUpdate();
    }
}
