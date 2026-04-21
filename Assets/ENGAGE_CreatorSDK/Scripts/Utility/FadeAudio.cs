using UnityEngine;
using System.Collections;

public class FadeAudio : MonoBehaviour {
	/// <summary>
	///Should be an audio source with static volume
	/// </summary>
	[Header("Audio Source should be set to full volume before using")]
	[Header("Functions can used on an object with an AudioSource attached")]
	public float fadeTime = 1;
    private void Start(){}
    //Call to fade the audio in
    public void FadeAudioIn () {}
	//Call to fade the audio out
	public void FadeAudioOut () {}
	
}
