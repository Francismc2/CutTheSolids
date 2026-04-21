using System;
using System.Collections.Generic;

namespace Engage.AFX.v1
{
    public class IndexedCollection
    {
        private readonly Dictionary<int, UnityEngine.Object> indexToObject = new();
        private readonly Dictionary<UnityEngine.Object, int> objectToIndex = new();
        private int currentIndex = 0;

        public void AddObject(UnityEngine.Object objectToAdd)
        {
            if (objectToAdd == null) throw new ArgumentNullException(nameof(objectToAdd));
            if (!objectToIndex.ContainsKey(objectToAdd))
            {
                indexToObject.Add(currentIndex, objectToAdd);
                objectToIndex.Add(objectToAdd, currentIndex);
                currentIndex++;
            }
        }

        public bool TryGetObject(UnityEngine.Object objectToGet, out int indexOut)
        {
            if (objectToGet == null) throw new ArgumentNullException(nameof(objectToGet));
            return objectToIndex.TryGetValue(objectToGet, out indexOut);
        }

        public bool TryGetObject(int index, out UnityEngine.Object objectOut)
        {
            return indexToObject.TryGetValue(index, out objectOut);
        }

        public void RemoveObject(UnityEngine.Object objectToRemove)
        {
            if (objectToRemove == null) return;

            if (objectToIndex.TryGetValue(objectToRemove, out int index))
            {
                objectToIndex.Remove(objectToRemove);
                indexToObject.Remove(index);
            }
        }

        public void RemoveObject(int index)
        {
            if (indexToObject.TryGetValue(index, out UnityEngine.Object objectToRemove))
            {
                indexToObject.Remove(index);
                objectToIndex.Remove(objectToRemove);
            }
        }
    }
}