using UnityEngine;
using System.Collections;

/// <summary>
/// Activate or deactivate child screens on screen objects
/// </summary>
public class TVScreenActivatorNew : MonoBehaviour {

	GameObject activeScreen;
	public GameObject tvScreenWeb;
    public GameObject tvScreenNotLoaded;

    public bool onlyShowLoadingScreen = false;
    public bool disableDuringScripts = false;
	public bool onlyShowWhenPlaying = false;
    

}
