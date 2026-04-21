using System;
using System.Collections.Generic;
using UnityEngine;
using Engage.AFX.v1;

namespace Engage.AFX.Anchor.v1
{
    public class AnchorSocketCollection : MonoBehaviour
    {
        [SerializeField] private List<AnchorSocket> socketList;
        private readonly IndexedCollection collection = new();

        public List<AnchorSocket> SocketList { get => socketList; }

        private void Awake()
        {
            foreach (var item in socketList)
            {
                collection.AddObject(item);
            }
        }

        public bool TryGetObject(AnchorSocket objectToGet, out int indexOut)
        {
            if (objectToGet == null) throw new ArgumentNullException(nameof(objectToGet));
            return collection.TryGetObject(objectToGet, out indexOut);
        }

        public bool TryGetObject(int index, out AnchorSocket objectOut)
        {
            UnityEngine.Object socket;
            if (collection.TryGetObject(index, out socket))
            {
                objectOut = (AnchorSocket)socket;
                return true;
            }

            objectOut = null;
            return false;
        }
    }
}