using UnityEngine;
using System.Collections.Generic;
namespace Engage.AFX.v1
{
    [AddComponentMenu("AFX/Path/Path Creator")]
    public class PathCreator : MonoBehaviour
    {
        public WrapMode wrapMode;
        public int lineRes = 1;
        public LineRenderer lineRenderer;
        public PathData pathData;
        public List<GameObject> pathEditPoints;
        public bool createPathMode;

        private void Update()
        {
            UpdatePathDataToMatchControlPoints();
            if (lineRenderer != null)
            {
                DrawPath();
            }
        }

        public void UpdatePathDataToMatchControlPoints()
        {
            if (pathEditPoints != null)
            {
                if (pathEditPoints.Count > 0 && pathData.path.KeyCount > 0)
                {
                    for (int i = 0; i < pathEditPoints.Count; i++)
                    {
                        if (pathEditPoints[i] != null)
                        {
                            pathData.path.AddPositionOnPath(i, pathEditPoints[i].transform.position);
                        }
                    }
                }
            }
        }

        private void DrawPath()
        {
            List<Vector3> positions = new List<Vector3>();
            if (lineRes <= pathData.path.KeyCount)
            {
                for (int i = 0; i < pathData.path.KeyCount; i++)
                {
                    positions.Add(pathData.path.GetPositionOnPath(i));
                }
            }
            else // smooth version
            {
                float adjust = 1f / lineRes;
                for (float i = 0; i < lineRes; i += adjust)
                {
                    positions.Add(pathData.path.GetPositionOnPath(i));
                }
            }

            int numOfPoints = positions.Count;
            lineRenderer.positionCount = numOfPoints;
            lineRenderer.SetPositions(positions.ToArray());
        }

        void OnDrawGizmos()
        {
            if (pathData != null)
            {
                if (this.enabled)
                {
                    pathData.path.SetWrapMode(wrapMode);
                    UpdatePathDataToMatchControlPoints();
                    if (lineRenderer != null)
                    {
                        DrawPath();
                    }
                }                
            }
        }
    }
}