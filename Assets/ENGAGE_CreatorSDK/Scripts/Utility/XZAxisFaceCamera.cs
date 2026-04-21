using UnityEngine;

public class XZAxisFaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3 (this.transform.position.x, Camera.main.transform.position.y, this.transform.position.z);
		transform.LookAt (targetPosition);
	}
}