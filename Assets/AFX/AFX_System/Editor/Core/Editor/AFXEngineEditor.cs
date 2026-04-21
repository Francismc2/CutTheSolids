using XNodeEditor;
using UnityEngine;
using UnityEditor;
namespace Engage.AFX.v1
{
    [InitializeOnLoad]
    [CustomEditor(typeof(AFXEngine))]
    public class AFXEngineEditor : Editor
    {
        AFXEngine afxEngine;
        NodeEditorWindow window;
        GUIStyle labelStyle;

        SerializedProperty graphProp;
        SerializedProperty GraphProp
        {
            get 
            {
                if (graphProp == null)
                {
                    graphProp = serializedObject.FindProperty("afxNodeGraph");
                }
                return graphProp;
            }
        }

        public AFXEngine AfxEngine
        {
            get
            {
                if (afxEngine == null)
                {
                    afxEngine = (AFXEngine)target;
                }
                return afxEngine;
            }
        }

        public void Awake()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
            graphProp = serializedObject.FindProperty(nameof(AfxEngine.AFXNodeGraph));
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            EditorApplication.update += RefreshNodeGraph;
            labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            EditorGUILayout.LabelField("Graph:", labelStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.PropertyField(GraphProp, new GUIContent(""));
            VersionLabels();
            if (GUILayout.Button("Edit Graph", GUILayout.MinHeight(50)))
            {
                OpenGraphInstance();
            }
            serializedObject.ApplyModifiedProperties();
        }

        public void OpenGraphInstance()
        {
            if (Application.isPlaying)
            {
                window = XNodeEditor.NodeEditorWindow.Open(AfxEngine.AfxNodeGraphRuntimeVersion);
                window.titleContent.text = AfxEngine.AfxNodeGraphRuntimeVersion.name + " - Runtime";
                window.Repaint();
            }
            else
            {
                window = XNodeEditor.NodeEditorWindow.Open(AfxEngine.AFXNodeGraph);
                window.titleContent.text = AfxEngine.AFXNodeGraph.name + " - Editor";
                window.Repaint();
            }
        }

        private void LogPlayModeState(PlayModeStateChange state)
        {
            bool isOpen;
            isOpen = EditorWindow.HasOpenInstances<NodeEditorWindow>();
            if (!isOpen) return;
            if (AfxEngine.AfxNodeGraphRuntimeVersion == null)
            {
                window = XNodeEditor.NodeEditorWindow.Open(AfxEngine.AFXNodeGraph);
            }
            else
            {
                window = XNodeEditor.NodeEditorWindow.Open(AfxEngine.AfxNodeGraphRuntimeVersion);
            }
        }

        public void RefreshNodeGraph()
        {
            if (Application.isPlaying && Application.isEditor)
            {
                if (window == null) return;
                window.Repaint();
            }
        }

        private void VersionLabels()
        {
            if (AfxEngine.AFXNodeGraph == null) return;

            if (AfxEngine.EngineVersion != AfxEngine.AFXNodeGraph.GraphVersion)
            {
                EditorGUILayout.LabelField("This Graph was not created with this version of AFX", labelStyle);
                EditorGUILayout.LabelField("Engine Version: " + AfxEngine.EngineVersion.ToString());
                EditorGUILayout.LabelField("Graph Version: " + AfxEngine.AFXNodeGraph.GraphVersion.ToString());
                EditorGUILayout.LabelField("This Graph is unlikly to work correctly unless used with an AFX Engine That matches it ", labelStyle);
            }
        }
    }
}