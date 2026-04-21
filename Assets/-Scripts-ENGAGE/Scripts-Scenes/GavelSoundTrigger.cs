using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GavelSoundTrigger : MonoBehaviour {

    public AudioClip gavelSound;
    AudioSource audSor;

    private void Start()
    {
       audSor = GetComponent<AudioSource>();
       audSor.clip = gavelSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        audSor.Play();
        Debug.Log("Gavel Noise Trigger");
    }
}
