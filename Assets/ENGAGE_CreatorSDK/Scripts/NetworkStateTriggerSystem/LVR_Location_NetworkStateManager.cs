using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVR_Location_NetworkStateManager : MonoBehaviour
{
    [Header("")]
    [Header("Simply List all of the network states for this location in this script")]
    [Header("If a state is to be removed, leave spot null in list")]
    [Header("The order in list should not be changed once published")]
    [Header("-----Network State Manager - ONLY ONE OF THESE IN LOCATION!----")]
    [Header("")]

    [Header("States to Manage")]
    public List<LVR_Location_NetworkState> networkStates = new List<LVR_Location_NetworkState>();

#if UNITY_ENGAGE
    IEnumerator Start()
    {
        while (ENG_IGM_NetworkStateController.instance == null)
            yield return null;

        yield return null;

        ENG_IGM_NetworkStateController.instance.InitializeStates(this);
    }
#endif

    public void UpdateState(LVR_Location_NetworkState state, int newValue)
    {
#if UNITY_ENGAGE
        if(ENG_IGM_NetworkStateController.instance)
            ENG_IGM_NetworkStateController.instance.UpdateState(state, newValue);
#endif
    }
}
