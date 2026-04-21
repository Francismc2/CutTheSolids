// /*-------------------------------------------
// ---------------------------------------------
// Creation Date: 05/08/22
// Author: tlynch
// Description: ENGAGE
// Engage XR
// ---------------------------------------------
// -------------------------------------------*/
using System;
using UnityEngine;

namespace Engage.AFX.v1
{
    [Serializable]
    public class Path
    {
        public int KeyCount { get { return pathX.keys.Length; } }
        public AnimationCurve pathX;
        public AnimationCurve pathY;
        public AnimationCurve pathZ;

        public Path()
        {
            pathX = new AnimationCurve();
            pathY = new AnimationCurve();
            pathZ = new AnimationCurve();
        }

        public Vector3 GetPositionOnPath(float time)
        {
            return new Vector3(pathX.Evaluate(time), pathY.Evaluate(time), pathZ.Evaluate(time));
        }

        public void SmoothTangentWeights(float weight)
        {
            for (int i = 0; i < KeyCount; i++)
            {
                pathX.SmoothTangents(i, weight);
                pathY.SmoothTangents(i, weight);
                pathZ.SmoothTangents(i, weight);
            }
        }

        public void SetWrapMode(WrapMode wrapMode)
        {
            pathX.postWrapMode = wrapMode;
            pathX.preWrapMode = wrapMode;

            pathY.postWrapMode = wrapMode;
            pathY.preWrapMode = wrapMode;

            pathZ.postWrapMode = wrapMode;
            pathZ.preWrapMode = wrapMode;
        }

        public void AddPositionOnPath(float time, Vector3 position)
        {
            if (pathX.AddKey(time, position.x) == -1)
            {
                int key = GetKeyAtTime(time, pathX);
                if (key != -1)
                {
                    ClearKey(key);
                    pathX.AddKey(time, position.x);
                }
            }
            if (pathY.AddKey(time, position.y) == -1)
            {
                int key = GetKeyAtTime(time, pathY);
                if (key != -1)
                {
                    ClearKey(key);
                    pathY.AddKey(time, position.y);
                }
            }

            if (pathZ.AddKey(time, position.z) == -1)
            {
                int key = GetKeyAtTime(time, pathZ);
                if (key != -1)
                {
                    ClearKey(key);
                    pathZ.AddKey(time, position.z);
                }
            }
        }

        public int GetKeyAtTime(float time, AnimationCurve animCurve)
        {
            if (animCurve.keys.Length > 0)
            {
                for (int i = 0; i < animCurve.keys.Length; i++)
                {
                    if (animCurve[i].time == time)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public int GetClosestKeyToTime(float time, AnimationCurve animCurve)
        {
            if (animCurve.keys.Length > 0)
            {
                if (time < .5f)
                {
                    return 0;
                }
                int closest = 1;
                for (int i = 0; i < animCurve.keys.Length; i++)
                {

                    if (Mathf.Abs(animCurve.keys[i].time - time) < Mathf.Abs(animCurve.keys[closest].time - time))
                    {
                        closest = i;
                    }
                }
                return closest;
            }
            return -1;
        }

        public void ClearKey(int key)
        {
            pathX.RemoveKey(key);
            pathY.RemoveKey(key);
            pathZ.RemoveKey(key);
        }

        public void ClearAllKeys()
        {
            for (int i = 0; i < pathX.keys.Length; i++)
            {
                pathX.RemoveKey(i);
                pathY.RemoveKey(i);
                pathZ.RemoveKey(i);
            }
        }
    }
}
