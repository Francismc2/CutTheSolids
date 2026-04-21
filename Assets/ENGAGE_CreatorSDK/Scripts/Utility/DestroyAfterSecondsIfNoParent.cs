using UnityEngine;
using System.Collections;

public class DestroyAfterSecondsIfNoParent : MonoBehaviour {
    /// <summary>
    /// If this object has no parent, destroy it after
    /// secondsUntilDestroy has passed.
    /// </summary>
	float secondsUntilDestroy = 8f;
	bool destroySet = false;
	// Update is called once per frame
	void Update () {
		if (transform.parent == null && !destroySet) {
			Invoke ("DestroyNow", secondsUntilDestroy);
			destroySet = true;
		}
	}
	void DestroyNow(){
		Destroy (gameObject);
	}
}
