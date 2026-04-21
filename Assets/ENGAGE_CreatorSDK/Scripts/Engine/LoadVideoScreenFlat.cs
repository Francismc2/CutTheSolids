using UnityEngine;
using System.Collections;

public class LoadVideoScreenFlat : MonoBehaviour {
    [Header("")]
    [Header("-----Special Options - normally all false-----")]
    [Header("")]
    public bool onlyShowLoadingScreen = false;//loading screen only
    public bool disableDuringScripts = false;//"scripts" are lessons/sequences
    public bool onlyShowWhenPlaying = false;//If paused or stopped will go away
}
