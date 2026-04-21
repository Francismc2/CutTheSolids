using UnityEngine;
using System.Collections;

public class TeleportTriggerMainCamera : MonoBehaviour {
	public float distanceFromCenterToTrigger;
	public GameObject teleportToPosition;
	public GameObject[] setObjectsInactive;
	public GameObject[] setObjectsActive;
	public GameObject playAudioSource;
	public GameObject stopAudioSource;
	public bool outfitOverrideOnTeleport = false;
	public int outfitOverrideInt;
}