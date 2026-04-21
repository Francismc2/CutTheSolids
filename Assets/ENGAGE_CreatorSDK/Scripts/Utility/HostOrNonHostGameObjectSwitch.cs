using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostOrNonHostGameObjectSwitch : MonoBehaviour
{
    [Header("Objects only Hosts will see")]
    public GameObject[] host_OnlyObjects;

    [Header("Objects only Non-Hosts will see")]
    public GameObject[] non_HostObjects;

    float timeDelay = 0.5f;
    float timeCounter = 0;
}
