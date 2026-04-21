using XNodeEditor;
using System;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(GetRegisteredObject))]
    public class GetRegisteredObjectNodeEditor : AFXNodeEditor
    {
        private GetRegisteredObject getRegisterNode;
        private string[] keys;
        SerializedProperty selectedIndexProperty;
        SerializedProperty selectedKeyProperty;

        public override void OnCreate()
        {
            if (getRegisterNode == null) getRegisterNode = target as GetRegisteredObject;

            selectedIndexProperty = serializedObject.FindProperty("selectedIndex");
            selectedKeyProperty = serializedObject.FindProperty("selectedKey");

            getRegisterNode.Graph.RegisteredObjectAdded += getRegisterNode.SetupPorts;
            getRegisterNode.Graph.RegisteredObjectRemoved += getRegisterNode.CleanUpPorts;
        }

        private void OnDestroy()
        {
            getRegisterNode.Graph.RegisteredObjectAdded -=  getRegisterNode.SetupPorts;
            getRegisterNode.Graph.RegisteredObjectRemoved -= getRegisterNode.CleanUpPorts;
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            UpdateDropdown();
        }

        public override string GetTitle()
        {
            if (!string.IsNullOrEmpty(selectedKeyProperty.stringValue))
            {
                return $"{selectedKeyProperty.stringValue} >";
            }

            return base.GetTitle();
        }

        public override int GetWidth()
        {
            return 125;
        }

        private void UpdateKeys()
        {
            keys = getRegisterNode.Graph.RegisteredObjects.Keys.ToArray();
            Array.Sort(keys);
        }

        private void UpdateDropdown()
        {
            if (getRegisterNode.Graph.RegisteredObjects.Count == 0) return;

            string buttonName = "Select";
            if (!string.IsNullOrEmpty(selectedKeyProperty.stringValue)) buttonName = selectedKeyProperty.stringValue;

            if (GUILayout.Button(buttonName, new GUIStyle("DropDown")))
            {
                UpdateKeys();
                NodeEditorWindow.current.onLateGUI += ()=> DrawMenu();
            }
        }

        private void DrawMenu()
        {
            GenericMenu menu = new GenericMenu();
            foreach (string key in keys)
            {
                menu.AddItem(new GUIContent(key), false, SetIndex, key);
            }

            Rect menuPos = new Rect(Event.current.mousePosition, new Vector2(0, 0));
            menu.DropDown(menuPos);
        }

        private void SetIndex(object key)
        {
            int index = Array.IndexOf(keys, (string)key);

            selectedIndexProperty.intValue = index;
            selectedKeyProperty.stringValue = keys[index];
            serializedObject.ApplyModifiedProperties();
            getRegisterNode.SetupPorts();
        }
    }
}