using UnityEngine;

public class DelayActivateObject : MonoBehaviour {
    /// <summary>
    /// Activate an object "child" after seconds. Does not change the object's
    /// original active state
    /// </summary>
	public float delayTime = 0;
	public GameObject child;
	float timenow = 0;
	bool childOn = false;

	
	// Update is called once per frame
	void Update () {
		if (timenow < delayTime) {
			timenow += Time.deltaTime;
		} else {
			if(!childOn)
			child.SetActive (true);
			childOn = true;
		}
	}
}
