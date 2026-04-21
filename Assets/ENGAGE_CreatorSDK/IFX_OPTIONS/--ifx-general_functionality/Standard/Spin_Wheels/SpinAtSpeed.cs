using UnityEngine;
using System.Collections;

public class SpinAtSpeed : MonoBehaviour {
	public float speedMultiplier = 5;
	public bool revForward = false;
	Vector3 lastPos;
	Vector3 currentPos;
	bool forwardMovement = true; // 1 for forward, -1 for backward
	Vector3 fwdPrev;
	Transform scaleComponent;
	// Use this for initialization
	void Start () {
		scaleComponent = transform.root;
		fwdPrev = transform.forward;
		StartCoroutine (Spin ());
	}

	// Update is called once per frame
	IEnumerator Spin () {
		lastPos = transform.position;
		while (gameObject.activeInHierarchy) {
            float sm = speedMultiplier;
			if (scaleComponent != null)sm *= (1 / scaleComponent.localScale.x);
			Vector3 movement = transform.position - lastPos;
			if (Vector3.Dot (fwdPrev, movement) < 0) {if (revForward) {forwardMovement = true;} else {forwardMovement = false;}} else {if (revForward) {forwardMovement = false;}else{forwardMovement = true;}}
            float dist = Vector3.Distance(lastPos, transform.position);
            if (dist != 0)
            {
                    if (forwardMovement)
                    {
                        transform.Rotate(Mathf.Clamp(dist * -sm,-999f,999f), 0, 0);
                    }
                    else {
                        transform.Rotate(Mathf.Clamp(dist * sm,-999f, 999f), 0, 0);
                    }
            }
			lastPos = transform.position;
			fwdPrev = transform.root.forward;
			yield return new WaitForSeconds (0);
		}
		yield return null;
	}
}

