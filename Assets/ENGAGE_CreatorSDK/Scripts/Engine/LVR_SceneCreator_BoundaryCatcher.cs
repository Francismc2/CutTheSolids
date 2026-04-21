using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach this script to the boundary cube object itself
 */
public class LVR_SceneCreator_BoundaryCatcher : MonoBehaviour
{

    GameObject playerSpawnPoint;
#if UNITY_ENGAGE
    private Vector3 m_spawnPos;
    private Collider m_boundCollider;
    bool resettingPosition;

    void Awake()
    {
        playerSpawnPoint = GameObject.Find("TheaterStartPosition");

        if(playerSpawnPoint == null)
            playerSpawnPoint = GameObject.Find("PlayerSpawnPosition");
    }

    void Start () {
        resettingPosition = false;
        if (GetComponent<Collider>() != null)
            m_boundCollider = GetComponent<Collider>();

            m_spawnPos = playerSpawnPoint.transform.position;

        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = false;
    }

	void Update () {
        TheaterVariables.teleportBoundaryCollider = m_boundCollider;

        if(Time.timeSinceLevelLoad > 6)
        {
            if (ENG_IGM_PlayerManager.instance != null && ENG_IGM_PlayerManager.instance.myPlayerObject != null && ENG_IGM_PlayerManager.instance.myPlayerObject.playerObject != null)
            {
                if (!m_boundCollider.bounds.Contains(ENG_IGM_PlayerManager.instance.myPlayerObject.playerObject.transform.position) && ENG_IGM_PlayerManager.instance.myPlayerObject.seatManagerScript.sitTriggerScript == null)
                {
                    if (!resettingPosition)
                    {
                        StartCoroutine(ResetPosition());
                        Debug.Log("Outside of Boundry: Resetting Player Position");
                    }
                }
            }
        }
    }

    IEnumerator ResetPosition()
    {
        resettingPosition = true;
        Instantiate(Resources.Load("HeadBoxTransitionQuick") as GameObject);
        yield return new WaitForSeconds(0.5f);
        ENG_IGM_PlayerManager.instance.myPlayerObject.playerObject.transform.position = m_spawnPos;
        resettingPosition = false;
    }
#endif
}
