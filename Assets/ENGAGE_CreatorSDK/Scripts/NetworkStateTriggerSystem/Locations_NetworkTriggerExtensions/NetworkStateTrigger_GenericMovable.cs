using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LVR_Location_NetworkState))]
public class NetworkStateTrigger_GenericMovable : MonoBehaviour
{

    private LVR_Location_NetworkState m_state;

    [Header("The object to move")]
    public GameObject go;

    [Header("Moves between Transforms, A (state 0) & B (state 1)")]
    public Transform transA;
    public Transform transB;

    [Header("Time to complete transition")]
    public float transitionTime = 1;

    [Header("Enable Objects while moving (example, object with audio)")]
    public GameObject[] setObjectsActiveIfMoving;

    [Header("Follow rule of Animation; Slow In, Slow Out")]
    public bool isSlowInSlowOut = true;

    private bool isMoving = false;
    private float distCovered;
    private float journeyLength;

    private Vector3 currentPos;
    private float lerpTarget;
    private float lerpValue;

    void Start()
    {
        m_state = GetComponent<LVR_Location_NetworkState>();
        journeyLength = getDistance(transA, transB);
    }

    public void ToggleState()
    {
        if (m_state.currentState == 0)
        {
            m_state.UpdateState(1);
        }

        else
        {
            m_state.UpdateState(0);
        }
    }

    public void SetState(int state)
    {
        m_state.UpdateState(state);
    }


    void SetObjectsActive(bool value)
    {
        if(setObjectsActiveIfMoving != null)
            for (int i = 0; i < setObjectsActiveIfMoving.Length; i++)
                if (setObjectsActiveIfMoving[i] != null)
                    if (setObjectsActiveIfMoving[i].activeSelf != value)
                        setObjectsActiveIfMoving[i].SetActive(value);
    }

    void ObjectUpdate()
    {
        if (go == null || go.transform == null)
            return;

        if (Time.timeSinceLevelLoad > 2)
        {

            lerpTarget = m_state.currentState;

            if (lerpTarget == 0)
            {
                if (lerpValue > lerpTarget)
                {
                    lerpValue -= Time.deltaTime / transitionTime;
                    if (!isMoving)
                    {
                        isMoving = true;
                        SetObjectsActive(true);
                    }
                }
                else
                {
                    if (isMoving)
                    {
                        isMoving = false;
                        SetObjectsActive(false);
                    }
                }
            }
            else
            {
                if (lerpValue < lerpTarget)
                {
                    lerpValue += Time.deltaTime / transitionTime;
                    if (!isMoving)
                    {
                        isMoving = true;
                        SetObjectsActive(true);
                    }
                }
                else
                {
                    if (isMoving)
                    {
                        isMoving = false;
                        SetObjectsActive(false);
                    }
                }

            }
            
            currentPos = go.transform.position;

            if (isSlowInSlowOut)
            go.transform.position = Vector3.Lerp(transA.position, transB.position, Mathf.SmoothStep(0,1, lerpValue));

            else
                go.transform.position = Vector3.Lerp(transA.position, transB.position, lerpValue);

            go.transform.rotation = Quaternion.Lerp(transA.rotation, transB.rotation, lerpValue);
        }
    }

    float getDistance(Transform t1, Transform t2)
    {
        float dist = Vector3.Distance(t1.position, t2.position);
        return dist;
    }

    void Update()
    {
        ObjectUpdate();
    }
}

