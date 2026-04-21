using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVR_StickySurface : MonoBehaviour {
	public Transform sticky_surface;
#if UNITY_ENGAGE
    void Start(){
		if(GetComponent<BoxCollider>() != null){
			GetComponent<BoxCollider>().isTrigger = true;
			gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		}

		// if(sticky_surface != null){
		// 	if(sticky_surface != null)	sticky_surface.gameObject.layer = LayerMask.NameToLayer("WorldGUI");
		// }

		if(sticky_surface == null){
			sticky_surface = this.gameObject.transform.Find("StickySurface");
			if(sticky_surface != null)	sticky_surface.gameObject.layer = LayerMask.NameToLayer("WorldGUI");
		}
		if(sticky_surface == null){
			sticky_surface = this.gameObject.transform.Find("WhiteboardDirectionObject");
			if(sticky_surface != null)	sticky_surface.gameObject.layer = LayerMask.NameToLayer("ScreenLighting");
		}

		if(sticky_surface != null){
			if(sticky_surface.GetComponent<Collider>() == null){
				BoxCollider bc = sticky_surface.gameObject.AddComponent<BoxCollider>();
				bc.isTrigger = true;
			}
		}
	}
#endif

}
