using UnityEngine;

namespace IVRE.whiteboard
{
    public class WbSurfaceDesc : MonoBehaviour
    {
        [Header("")]
        [Header("--Unique ID in location - Only set this once! Will affect recorded data--")]
        [Header("")]
        public string uniqueID = "WB2-{UNIQUE_ID_HERE}";

        [Header("")]
        [Header("-Other Variables - Background is optional")]
        [Header("")]
        public Transform uiAnchor;
        public float aspect = 1.0f;
        public Texture2D background;
        public GameObject messageRoot;
        public float disconnectDistance = 4.0f;
        public GameObject activateButton;
        public MeshRenderer paintMesh;
        public MeshRenderer[] optionalProjectedMeshes;
        public Color clearColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        public Color inkColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        public bool useWorldLighting;
        public Transform penPosition;

#if UNITY_ENGAGE
        private WbSurface surface;
        private void Start()
        {
            if (surface == null) {
                surface = gameObject.AddComponent<WbSurface>();
                surface.init(this);
            }
            //---make so connect button works in world ui system------------------
            if (!gameObject.GetComponent<LVR_CanvasWorldCamera>())
                gameObject.AddComponent<LVR_CanvasWorldCamera>();
        }
#endif
    }
}