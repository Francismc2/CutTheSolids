using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Goes onto the spray component
/// </summary>
public class LVR_Effect4D_Trigger_Special_FireExtinguishSprayTrigger : MonoBehaviour {

    [System.Serializable]
    public enum ExtinguisherType
    {
        None = 0,
        CO2 = 1,
        Foam = 2,
        Powder = 3,
        Blanket = 4
    }
    public ExtinguisherType extinguisherType;
}
