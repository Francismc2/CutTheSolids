using XNodeEditor;
using UnityEngine;
using UnityEditor;
using XNode;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(RegisterObject))]
    public class RegisterObjectNodeEditor : AFXNodeEditor
    {
        private RegisterObject registerNode;
        private SerializedProperty registerAsProperty;
        private NodePort port;
        private string lastEnteredText;

        public override void OnCreate()
        {
            if (registerNode == null) registerNode = target as RegisterObject;
            port = registerNode.GetInputPort("input");

            registerAsProperty = serializedObject.FindProperty("key");
            lastEnteredText = registerAsProperty.stringValue;
        }

        public override string GetTitle()
        {
            NodeEditorResources.styles.nodeHeader.wordWrap = true;
            if (!string.IsNullOrEmpty(registerNode.Key) && port.IsConnected)
            {
                return $"> {registerNode.Key}";
            }

            return "Register Object";
        }

        public override Color GetAccentTint()
        {
            if (port.IsConnected)
            {
                return GetPortColor(port.Connection);
            }

            return base.GetAccentTint();
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            serializedObject.Update();

            EditorGUILayout.LabelField("Key:");
            registerAsProperty.stringValue = EditorGUILayout.TextField(registerAsProperty.stringValue);

            serializedObject.ApplyModifiedProperties();

            if (lastEnteredText != registerAsProperty.stringValue)
            {
                port.ClearConnections();
                registerNode.CleanUp();
            }

            lastEnteredText = registerAsProperty.stringValue;
        }

        public override int GetWidth()
        {
            return 125;
        }
    }
}