using UnityEngine;

namespace Engage.Utility
{
    public class MobileOrDesktopPlatformGameObjectSwitch : MonoBehaviour
    {
        [Header("ANDROID Actually Means ANY Mobile Platform")]
        public GameObject[] androidOnlyObjects;
        public GameObject[] desktopOnlyObjects;

        private void OnEnable()
        {
#if UNITY_ANDROID || UNITY_IOS
        {
            foreach (GameObject obj in androidOnlyObjects)
                if(obj != null)
                    if(obj.activeSelf != true)
                        obj.SetActive(true);
            foreach (GameObject obj in desktopOnlyObjects)
                if(obj != null)
                    if(obj.activeSelf != false)
                        obj.SetActive(false);
        }
#else
            {
                foreach (GameObject obj in desktopOnlyObjects)
                    if (obj != null)
                        if (obj.activeSelf != true)
                            obj.SetActive(true);
                foreach (GameObject obj in androidOnlyObjects)
                    if (obj != null)
                        if (obj.activeSelf != false)
                            obj.SetActive(false);
            }
#endif
        }
    }
}
