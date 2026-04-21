using UnityEngine;

public class YAxisFaceCamera : MonoBehaviour {
    Vector3 targetPosition = Vector3.zero;
	
	void Update () {
        targetPosition = Camera.main.transform.position;
        targetPosition.y = transform.position.y;
		transform.LookAt (targetPosition);
	}
}
