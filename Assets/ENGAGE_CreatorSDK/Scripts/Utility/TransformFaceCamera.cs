using UnityEngine;

public class TransformFaceCamera : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Camera.main.transform.position);
	}
}
