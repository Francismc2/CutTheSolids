using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTransformLerp : MonoBehaviour
{
    public GameObject go;
    public Transform transA;
    public Transform transB;
    public float transitionTime = 1;
    [SerializeField]
    private bool useLocalValues = true;

    public GameObject[] setObjectsActiveIfMoving;

    public bool isSlowInSlowOut = true;

    public bool goingA = true;

    private bool isMoving = false;
    private float lerpTarget;
    private float lerpValue;

    void SetObjectsActive(bool value)
    {
        if (setObjectsActiveIfMoving != null)
            for (int i = 0; i < setObjectsActiveIfMoving.Length; i++)
                if (setObjectsActiveIfMoving[i] != null)
                    if (setObjectsActiveIfMoving[i].activeSelf != value)
                        setObjectsActiveIfMoving[i].SetActive(value);
    }

    public void ToggleGoingA()
    {
        goingA = !goingA;
    }

    public void SetGoingA(bool AorB)
    {
       goingA = AorB;
    }

    void ObjectUpdate()
    {
        if (go == null || go.transform == null)
            return;

        if (Time.timeSinceLevelLoad > 2)
        {
            if (goingA)
                lerpTarget = 0;
            else
                lerpTarget = 1;

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

            if (isSlowInSlowOut)
            {
                if (useLocalValues)
                {
                    go.transform.localPosition = Vector3.Lerp(transA.localPosition, transB.localPosition, Mathf.SmoothStep(0, 1, lerpValue));
                }
                else
                {
                    go.transform.position = Vector3.Lerp(transA.position, transB.position, Mathf.SmoothStep(0, 1, lerpValue));
                }
            }
            else
            {
                if (useLocalValues)
                {
                    go.transform.localPosition = Vector3.Lerp(transA.localPosition, transB.localPosition, lerpValue);
                }
                else
                {
                    go.transform.position = Vector3.Lerp(transA.position, transB.position, lerpValue);
                }
            }

            if (useLocalValues)
            {
                go.transform.localRotation = Quaternion.Lerp(transA.localRotation, transB.localRotation, lerpValue);
            }
            else
            {
                go.transform.rotation = Quaternion.Lerp(transA.rotation, transB.rotation, lerpValue);
            }
        }
    }

    float getDistance(Transform t1, Transform t2)
    {
        float dist = useLocalValues ? Vector3.Distance(t1.localPosition, t2.localPosition) : Vector3.Distance(t1.position, t2.position);
        return dist;
    }

    void Update()
    {
        ObjectUpdate();
    }
}
