using UnityEditor;
using UnityEngine;

namespace Engage.AFX.v1
{
    public static class AFXStyles
    {
        public static GUIStyle Error 
        {
            get 
            { 
                return new GUIStyle(EditorStyles.label)
                {
                    wordWrap = true,
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold
                };
            } 
        }
    }
}