using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableByDistance : MonoBehaviour {
    public float dist = 1.5f;

    public GameObject ob;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckDistance", 0.0f, 0.1f);
	}

    void CheckDistance()
    {
#if UNITY_ENGAGE
        if (Engage_ScriptHelper.mainCamera != null)
        {
            if (Vector3.Distance(ob.transform.position, Engage_ScriptHelper.mainCamera.transform.position) > dist)
#else
        {
            if (Vector3.Distance(ob.transform.position, Camera.main.transform.position) > dist)
#endif
            {
                if (ob.activeSelf)
                    ob.SetActive(false);
            }

            else
            {
                if (!ob.activeSelf)
                    ob.SetActive(true);
            }
        }
    }
}
