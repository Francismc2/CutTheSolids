using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to Toggle Objects with States
/// </summary>

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_ToggleObjects : MonoBehaviour {

    private LVR_Location_NetworkState m_state;

    [Header("Objects to Toggle")]
    public List< GameObject> go = new List<GameObject>();

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

    void ObjectUpdate()
    {
      if (m_state.currentState == 0)
        {
            foreach (GameObject o in go)
            {
                o.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject o in go)
            {
                o.SetActive(true);
            }
        }
    }

    private void Update()
    {
        ObjectUpdate();
    }
}
