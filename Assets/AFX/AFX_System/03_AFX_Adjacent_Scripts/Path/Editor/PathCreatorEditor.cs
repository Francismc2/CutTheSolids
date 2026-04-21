using UnityEngine;
using UnityEditor;
namespace Engage.AFX.v1
{
    [CustomEditor(typeof(PathCreator))]
    public class PathCreatorEditor : Editor
    {
        PathCreator afxPathCreator;

        void OnSceneGUI()
        {
            if (afxPathCreator == null)
            {
                afxPathCreator = (PathCreator)target;
            }

            if (!afxPathCreator.enabled)
            {
                return;
            }

            EditorUtility.SetDirty(afxPathCreator);

            if (afxPathCreator.pathData == null)
            {
                ClearEditPoints();
            }

            SceneView scene = SceneView.lastActiveSceneView;
            Event e = Event.current;

            if (afxPathCreator.createPathMode)
            {
                if (e.type == EventType.MouseDown && e.button == 2)
                {
                    //Debug.Log("Middle Mouse was pressed");
                    CreatePathGuideObject(scene, e.mousePosition);
                    e.Use();
                }
            }

            afxPathCreator.UpdatePathDataToMatchControlPoints();
        }

        private void CreatePathGuideObject(SceneView scene, Vector3 mousePos)
        {
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = scene.camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = SpawnPathEditObject(afxPathCreator.transform.InverseTransformPoint(hit.point));
                if (afxPathCreator.pathEditPoints.Count > 0)
                {
                    if (hit.transform.gameObject == afxPathCreator.pathEditPoints[0])
                    {
                        go.transform.SetParent(afxPathCreator.pathEditPoints[0].transform, false);
                    }
                }
                afxPathCreator.pathData.path.AddPositionOnPath(afxPathCreator.pathData.path.KeyCount, afxPathCreator.transform.InverseTransformPoint(go.transform.localPosition));
            }
            else
            {
                GameObject go = SpawnPathEditObject(afxPathCreator.transform.InverseTransformPoint(ray.GetPoint(5)));
                afxPathCreator.pathData.path.AddPositionOnPath(afxPathCreator.pathData.path.KeyCount, afxPathCreator.transform.InverseTransformPoint(go.transform.localPosition));
            }
        }

        private GameObject SpawnPathEditObject(Vector3 position)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.parent = afxPathCreator.transform;
            go.transform.localPosition = position;
            go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            afxPathCreator.pathEditPoints.Add(go);
            go.name = "EditPoint " + afxPathCreator.pathEditPoints.Count.ToString();
            return go;
        }

        private void ClearEditPoints()
        {
            if (afxPathCreator.pathEditPoints != null)
            {
                foreach (GameObject item in afxPathCreator.pathEditPoints)
                {
                    if (item != null)
                    {
                        DestroyImmediate(item);
                    }

                }
                afxPathCreator.pathEditPoints.Clear();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (afxPathCreator == null)
            {
                afxPathCreator = (PathCreator)target;
            }
            DrawDefaultInspector();
            if (afxPathCreator.createPathMode)
            {
                if (GUILayout.Button("Exit Create Path Mode"))
                {
                    afxPathCreator.createPathMode = false;
                }
            }
            else
            {
                if (GUILayout.Button("Create Path Mode"))
                {
                    afxPathCreator.createPathMode = true;
                }
            }

            if (afxPathCreator.lineRenderer == null)
            {
                if (GUILayout.Button("Create Preview"))
                {
                    if (afxPathCreator.lineRenderer == null)
                    {
                        afxPathCreator.lineRenderer = afxPathCreator.GetComponent<LineRenderer>();
                        if (afxPathCreator.lineRenderer == null)
                        {
                            afxPathCreator.lineRenderer = afxPathCreator.gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
                        }
                    }
                    afxPathCreator.lineRenderer.startWidth = 0.2f;
                    afxPathCreator.lineRenderer.endWidth = 0.2f;
                }
            }
            else
            {
                if (GUILayout.Button("Remove Preview"))
                {
                    DestroyImmediate(afxPathCreator.GetComponent<LineRenderer>());
                }
            }
            if (afxPathCreator.pathEditPoints.Count > 0)
            {
                if (GUILayout.Button("Remove Edit points"))
                {
                    ClearEditPoints();
                }
            }
            else
            {
                if (GUILayout.Button("Create Edit points"))
                {

                    ClearEditPoints();
                    for (int i = 0; i < afxPathCreator.pathData.path.KeyCount; i++)
                    {
                        SpawnPathEditObject(afxPathCreator.pathData.path.GetPositionOnPath(i));
                    }
                }
            }

            if (GUILayout.Button("Clear Path Data"))
            {
                ClearEditPoints();
                afxPathCreator.pathData.path = new Path();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
