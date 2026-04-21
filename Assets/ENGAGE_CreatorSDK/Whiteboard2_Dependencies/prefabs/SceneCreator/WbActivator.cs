using IVRE.whiteboard;
using UnityEngine;

public class WbActivator : MonoBehaviour {
#if UNITY_ENGAGE
    public WbSurface surface;
#endif
    public void onPress()
    {
#if UNITY_ENGAGE
        if (WbUi.I.surface == surface) {
        } else {
            //dont connect if already connected to a surface
            if (WbUi.I.surface != null) {
                return;
            }
            //only connect if nearer than disconnect distance, otherwise will
            //disconnect immediatley
            Ray lookRay = WbUi.I.input.getLookRay();
            float distance = (surface.transform.position - lookRay.origin).magnitude;
            if (distance < surface.d.disconnectDistance *0.9f) {
                WbBoss.I.connectToSurface(surface,0,true);
            }
        }
#endif
    }
}
