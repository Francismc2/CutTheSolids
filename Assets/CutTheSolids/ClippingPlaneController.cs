using UnityEngine;

public class ClippingPlaneController : MonoBehaviour
{
    public Material targetMaterial;

    void Update()
    {
        Vector3 planePosition = transform.position;
        Vector3 planeNormal = transform.up;

        targetMaterial.SetVector("_PlanePosition", planePosition);
        targetMaterial.SetVector("_PlaneNormal", planeNormal);
    }
}