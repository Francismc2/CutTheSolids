using UnityEngine;
using System.Collections;

public class RotateTransform : MonoBehaviour {
	
	public Vector3 Speed = new Vector3(0,20f,0);


	void Update () {
 		Quaternion offset = Quaternion.Euler(Speed * Time.deltaTime);
		transform.localRotation = transform.localRotation*offset;
	}
	
}
