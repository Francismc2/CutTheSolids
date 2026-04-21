using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class LVR_Effect4D_ui_text_option : MonoBehaviour {
    public Text text;
    [System.Serializable]
    public class OnGoLoaded : UnityEvent<string> { }

    public OnGoLoaded onGoLoaded;
}
