using UnityEngine;
using UnityEditor;
namespace Engage.AFX.v1
{
    [CustomEditor(typeof(AFXObjectReferenceManager))]
    public class ObjectReferenceManagerEditor : Editor
    {
        private AFXObjectReferenceManager objRefManager;
        private AFXGraph lastGraph;

        void OnEnable()
        {
            if (objRefManager == null)
            {
                objRefManager = (AFXObjectReferenceManager)target;
            }

            RefreshSlots();
        }

        public override void OnInspectorGUI()
        {
            var labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };



            if (objRefManager.AfxEngine.AFXNodeGraph != null)
            {
                if (objRefManager.AfxEngine.AFXNodeGraph != lastGraph)
                {
                    lastGraph = objRefManager.AfxEngine.AFXNodeGraph;
                    objRefManager.AfxEngine.AFXNodeGraph.RequiresRefSlotRefresh = true;
                }

                if (objRefManager.AfxEngine.AFXNodeGraph.RequiresRefSlotRefresh)
                {
                    objRefManager.AfxEngine.AFXNodeGraph.RequiresRefSlotRefresh = false;
                    RefreshSlots();
                }
            }
            else
            {
                objRefManager.ResetReferenceSlots();
            }

            serializedObject.UpdateIfRequiredOrScript();
            SerializedProperty List = serializedObject.FindProperty("objectRefsEngine");

            foreach (SerializedProperty element in List)
            {
                SerializedProperty refName = element.FindPropertyRelative("referenceName");
                SerializedProperty slotType = element.FindPropertyRelative("typeAsString");
                SerializedProperty refvalue = element.FindPropertyRelative("referenceValue");

                if (System.Type.GetType(slotType.stringValue) == null)
                {
                    RefreshSlots();
                }

                EditorGUILayout.LabelField(refName.stringValue, labelStyle, GUILayout.ExpandWidth(true));
                refvalue.objectReferenceValue = EditorGUILayout.ObjectField(refvalue.objectReferenceValue, System.Type.GetType(slotType.stringValue), true);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RefreshSlots()
        {
            Undo.RecordObject(objRefManager, "Refresh of AFX ObjectManger Slots");
            objRefManager.RefreshReferenceSlots();
            PrefabUtility.RecordPrefabInstancePropertyModifications(objRefManager);
        }
    }
}