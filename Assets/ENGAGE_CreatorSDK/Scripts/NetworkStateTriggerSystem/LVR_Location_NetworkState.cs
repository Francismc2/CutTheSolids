using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class LVR_Location_NetworkState : MonoBehaviour
{
    [Header("")]
    [Header("Don't ever change order in list once added.")]
    [Header("Add to list in Location_NetworkStateManager")]
    [Header("Write using Update State function")]
    [Header("Read value from current state variable,")]
    [Header("-----Network State Script----")]
    [Header("")]

    public LVR_Location_NetworkStateManager networkStateManager;

    [Header("Random/Unique 5-10 digit number for this state")]
    public int stateID;

    [Header("i.e.: Stage Lighting")]
    [Header("Pretty title (for users) for this state (~15 letters if needed)")]
    public string prettyName;

    [Header("Should this state not be added to recordings")]
    public bool dontRecordState;

    [Header("Limit this text to <200 characters")]
    [Header("Describe to users what this state does in the scene, how it is triggered")]

    public string description;

    [Header("The current state. Typically default of zero if possible.")]
    public int currentState = 0;

    public void UpdateState(int newValue)
    {
        if(networkStateManager != null && networkStateManager.networkStates.Contains(this))
            networkStateManager.UpdateState(this, newValue);
    }

    public void IncrementState() { }
    public void DecrementState() { }
}
