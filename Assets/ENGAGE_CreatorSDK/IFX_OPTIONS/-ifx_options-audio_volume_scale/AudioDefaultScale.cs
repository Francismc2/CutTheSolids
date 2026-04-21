using UnityEngine;
using Engage.IFX;

public class AudioDefaultScale : MonoBehaviour {
    // Use this for initialization
    bool isConverted = false;
    bool is3D = true;
    [Header("Audio Clip to play")]
    [Header("")]
    [Header("These values will be read from the Audio Source if it exists")]
    [Header("")]
    [Header("New Audio IFX only need this script on an empty GameObject.")]
    public AudioClip _clip;
    [Header("Default to loop?")]
    public bool loop_default;
    public float pitch_default = 1;


    

}