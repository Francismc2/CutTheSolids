using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RotateTransform))]
public class ChangeRotateTransformSpeed : MonoBehaviour {
    /// <summary>
    /// Use with RotateTransform script to change the speed over time.
    /// Originally made for Ted Talks (Earth population / rotation)
    /// </summary>

    
	[System.Serializable]
	public class RotateTransformSpeedRamp
	{
	public float secondsStart; // what time to start this speed ramp
	public float secondsEnd; // waht time to end this speed ramp
	public Vector3 rotateSpeedStart; //rotation speed at start
	public Vector3 rotateSpeedEnd; //rotation speed at end
	}
	public List<RotateTransformSpeedRamp> speedRamps = new List<RotateTransformSpeedRamp>();


	// Use this for initialization
	void Start () {
		foreach(RotateTransformSpeedRamp sr in speedRamps){
			StartCoroutine(ChangeRotation(sr.secondsStart,sr.secondsEnd,sr.rotateSpeedStart,sr.rotateSpeedEnd));
		}
	}

	IEnumerator ChangeRotation(float secStarts,float secEnd,Vector3 speedStart,Vector3 speedEnd){
		float t = secEnd - secStarts;
		yield return new WaitForSeconds (secStarts);
		GetComponent<RotateTransform> ().Speed = speedStart;
		float i = 0;
		while (i < 1) {
			i += Time.deltaTime / t;
			GetComponent<RotateTransform> ().Speed = Vector3.Lerp(speedStart,speedEnd,i);
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}
}