using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PerfectSeatName : MonoBehaviour
{
    [Header("Objects to do task on")]
    public List<GameObject> objects;

    [Header("Initial number to be added at end of name")]
    public int startNum = 2;

    [Header("Name to change all objects")]
    public string nameToBe = "PerfectSeat";

    [Header("Click to start task")]
    public bool startProcess = false;

    [Header("Renable Objects disabled during task")]
    public bool renableObjects = false;

    [Header("Toggle disabling objects after changing names")]
    public bool disableObjectAfter = false;

    [Header("Update start num to next number in sequence after task")]
    public bool updateStartNumAfter = false;

    [Header("Remove objects from list that have had names changed")]
    public bool removeFromListAfter = false;

    private int m_objectNum = 0;

    [Header("Objects disabled during task")]
    public List<GameObject> m_obsToEnable;
    private GameObject m_tempObj;

    void Update()
    {
        if (startProcess)
        {
            m_objectNum = startNum;

            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].GetComponentInChildren<LVR_SitTrigger>())
                {
                    objects[i].GetComponentInChildren<LVR_SitTrigger>().gameObject.name = nameToBe + m_objectNum;
                }

                if (disableObjectAfter)
                {
                    objects[i].SetActive(false);
                    m_obsToEnable.Add(objects[i]);
                }

                m_objectNum++;
            }

            if (updateStartNumAfter)
                startNum = m_objectNum;

            if (removeFromListAfter)
                objects.Clear();

            m_objectNum = 0;

            startProcess = false;
        }

        if (renableObjects)
        {
            foreach (GameObject go in m_obsToEnable)
            {
                go.SetActive(true);
            }
            m_obsToEnable.Clear();

            renableObjects = false;
        }
    }
}
