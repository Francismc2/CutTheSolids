using UnityEngine;
using UnityEngine.UI;

namespace Engage.WebViews
{
    public class PersonalBrowserMaterial : MonoBehaviour 
    {
        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

#if UNITY_EDITOR
        public void Reset()
        {
            Renderer _renderer = GetComponent<Renderer>();
            RawImage _rawImg = GetComponent<RawImage>();

            if (_renderer == null && _rawImg == null)
            {
                if (UnityEditor.EditorUtility.DisplayDialog("Choose a Component", "You are missing one of the required components. Please choose one to add", "Renderer", "RawImage"))
                {
                    gameObject.AddComponent<Renderer>();
                }
                else
                {
                    gameObject.AddComponent<RawImage>();
                }
            }
        }
#endif
    }
}
