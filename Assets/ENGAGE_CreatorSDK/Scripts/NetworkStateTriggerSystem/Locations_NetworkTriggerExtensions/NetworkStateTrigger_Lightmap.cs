using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_Lightmap : MonoBehaviour {

    private LVR_Location_NetworkState m_state;

    
    [System.Serializable]
    public class LightingModes
    {
        public string name;
        public int index;
        public bool useEmissive;
        public bool dontCycle = false; //This is whether a lightmap can be used through the Cycle() function
        public int lightObjectsIndexToTurnOn;
        public Texture2D[] directionalMaps;
        public Texture2D[] colorMaps;
        public LightProbes lightProbes;
    }

    [Header("List of SO Lightmaps")]
    public List<LightingModes> lightingModes; //Number of used lightmaps

    [Header("Objects to enable along with Lightmap")]
    public List<GameObject> lightObjects;
    public Material[] emissionMaterials;

    private int lastLight;

    void Start () {
        m_state = GetComponent<LVR_Location_NetworkState>();
        lastLight = m_state.currentState;
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

    public void CycleStates()
    {
        for (int i = m_state.currentState; i < lightingModes.Count; i++)
        {
            if (i == lightingModes.Count - 1)
            {
                m_state.UpdateState(0);
                break;
            }
            else
            {
                if (lightingModes[i + 1].dontCycle == false)
                {
                    m_state.UpdateState(i++);
                    break;
                }
            }
        }
    }


    /*
     * Unity buttons only allow functions with 1 parameter
     * 5 is hardcoded time value atm (other elements take 4 seconds to move)
     */
    public void EnableCinemaMode(int state)
    {
        StartCoroutine(WaitTimeStateChange(state));
    }

    public IEnumerator WaitTimeStateChange(int state)
    {
        yield return new WaitForSeconds(5);
        m_state.UpdateState(state);
    }

    public void ChangeLightmaps(int index)
    {
        m_state.UpdateState(index);
    }

    private void FixedUpdate()
    {
      if (m_state.currentState != lastLight)
        {
            StartCoroutine(ChangeLightmapsRoutine(m_state.currentState));
            lastLight = m_state.currentState;
        }
    }

    private IEnumerator ChangeLightmapsRoutine(int indexToUse)
    {
        for (int i = 0; i < lightObjects.Count; i++)
        {
            if (lightingModes[indexToUse].lightObjectsIndexToTurnOn == i)
            {
                lightObjects[i].SetActive(true);
            }
            else
            {
                lightObjects[i].SetActive(false);
            }
        }

        for (int i = 0; i < emissionMaterials.Length; i++)
        {
            if (lightingModes[indexToUse].useEmissive)
                emissionMaterials[i].EnableKeyword("_EMISSION");
            else
                emissionMaterials[i].DisableKeyword("_EMISSION");
        }

        int lightMapCount = lightingModes[0].directionalMaps.Length;
        LightmapData[] lightmapData = new LightmapData[lightMapCount];

        for (int i = 0; i < lightMapCount; i++)
            lightmapData[i] = new LightmapData();

        for (int i = 0; i < lightMapCount; i++)
        {
            lightmapData[i].lightmapColor = lightingModes[indexToUse].colorMaps[i];
            lightmapData[i].lightmapDir = lightingModes[indexToUse].directionalMaps[i];

        }

        // Switch Light Probes
        if (lightingModes[indexToUse].lightProbes != null)
            LightmapSettings.lightProbes = lightingModes[indexToUse].lightProbes;
        else
            LightmapSettings.lightProbes = null;

        LightmapSettings.lightmaps = lightmapData;

        yield return null;
    }
}
