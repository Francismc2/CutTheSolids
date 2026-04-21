using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class NetworkObjectRespawnManager : MonoBehaviour
{
    public GameObject[] networkObjectArray;

    //tidy up access
    public Vector3[] m_objectSpawnPosArray;
    public Quaternion[] m_objectSpawnRotArray;

    public GameObject boundary;

    private Collider m_boundaryCollider;

   // public List<LVR_DynamicPhysicsChildTrigger> m_physicsObsList = new List<LVR_DynamicPhysicsChildTrigger>();

    void Start()
    {
        if (boundary.GetComponent<Collider>() != null)
            m_boundaryCollider = boundary.GetComponent<Collider>();


        if (boundary.GetComponent<MeshRenderer>() != null)
            boundary.GetComponent<MeshRenderer>().enabled = false;

        ////Get a list of names of gameobjects related to this particular mini-game
        //for (int i = 0; i < networkObjectArray.Length; i++)
        //{
        //    if (networkObjectArray[i] != null)
        //    {
        //        m_networkObjsNameArray[i] = networkObjectArray[i].name;
        //    }
        //}

       
        

        /*
         *      How does it know what objectToParent?
         *      TransferNetworkObject netTransScript = objectToParent.GetComponent<TransferNetworkObject>();
                SinglePlayerPickupObject spScript = objectToParent.GetComponent<SinglePlayerPickupObject>();
         */
    }


    void Update()
    {
        CreateNetworkObjectReference();
        CheckCollisions();
    }

    void CheckCollisions()
    {
        //for (int i = 0; i < m_physicsObsArray.Length; i++)
        //{
        //    if (m_physicsObsArray[i] != null)
        //    {
        //        if (!m_boundaryCollider.bounds.Contains(m_physicsObsArray[i].transform.position))
        //        {
        //            ResetObject(i);
        //        }
        //    }
        //}
    }

    void CreateNetworkObjectReference()
    {
        //if ((Time.timeSinceLevelLoad > 2.0f) && (m_physicsObsList == null))
        //{
        //    m_physicsObsList = new List<LVR_DynamicPhysicsChildTrigger>(FindObjectsOfType<LVR_DynamicPhysicsChildTrigger>());


        //    if (m_physicsObsList != null)
        //    {
        //        foreach (LVR_DynamicPhysicsChildTrigger obj in m_physicsObsList)
        //        {
        //            for (int i = 0; i < m_networkObjsNameArray.Length; i++)
        //            {
        //                if (obj.physicsObjectRoot.name == m_networkObjsNameArray[i])
        //                {
        //                    m_physicsObsListParents.Add(obj);
        //                }
        //            }
        //        }
        //    }
        //}
/*
        if ((Time.timeSinceLevelLoad > 2.0f) && (networkObjectArray != null) && (m_physicsObsList.Count == 0))
        {
            for (int i = 0; i < networkObjectArray.Length; i++)
            {
                if (networkObjectArray[i].GetComponentInChildren<LVR_DynamicPhysicsChildTrigger>() != null)
                m_physicsObsList.Add(networkObjectArray[i].GetComponentInChildren<LVR_DynamicPhysicsChildTrigger>());
            }

            if (m_physicsObsList.Count != 0)
            {
                m_objectSpawnPosArray = new Vector3[m_physicsObsList.Count];
                m_objectSpawnRotArray = new Quaternion[m_physicsObsList.Count];

                for (int i = 0; i < m_physicsObsList.Count; i++)
                {
                    m_objectSpawnPosArray[i] = m_physicsObsList[i].transform.position;
                    m_objectSpawnRotArray[i] = m_physicsObsList[i].transform.rotation;
                }
            }
        }
        */
    }

    void ResetObject(int i)
    {
        //if (m_physicsObsArray[i] != null)
        //{
            
        //    //if (m_physicsObsArray[i].transform.parent.gameObject != null)
        //    //{
        //    //    Debug.Log("Object Parent Name: " + m_physicsObsArray[i].transform.parent.gameObject.name);
        //    //    m_physicsObsArray[i].transform.parent.gameObject.transform.position = m_objectSpawnPosArray[i];
        //    //    m_physicsObsArray[i].transform.parent.gameObject.transform.rotation = m_objectSpawnRotArray[i];
        //    //}
        //    //else
        //    //{
        //    //    Debug.Log("NetworkObjectArray Parent object empty at index: " + i);
        //    //}
        //}
    }

}
