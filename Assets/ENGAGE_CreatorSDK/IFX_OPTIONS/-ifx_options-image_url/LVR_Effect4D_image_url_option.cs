using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class LVR_Effect4D_image_url_option : MonoBehaviour
{
    [System.Serializable]
    public class OnGoLoaded : UnityEvent<string> { }

    public OnGoLoaded onGoLoaded;
}
