using UnityEngine;

public class KeepTransformAtLossyScale : MonoBehaviour {
    public Vector3 maintainLossyScale;

	void Update () {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(maintainLossyScale.x / transform.lossyScale.x, maintainLossyScale.y / transform.lossyScale.y, maintainLossyScale.z / transform.lossyScale.z);
    }
}
