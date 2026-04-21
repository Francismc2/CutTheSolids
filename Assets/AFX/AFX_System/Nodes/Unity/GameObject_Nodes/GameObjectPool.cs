using UnityEngine;
using System.Collections.Generic;
using XNode;
using System.Linq;

namespace Engage.AFX.v1.Instantiate
{
    [NodeTitle("GameObject Pool")]
    [CreateNodeMenu(AFXMenuTree.Instantiate + "GameObject Pool")]
    public class GameObjectPool : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private GameObject prefab;
        [SerializeField][Input(ShowBackingValue.Never)] private Transform parent;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int poolSize;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 spawnPosition;
        [SerializeField][Input(ShowBackingValue.Never)] private Quaternion spawnRotation;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 spawnScale = Vector3.one;

        [SerializeField][Output] private List<GameObject> objectPool;
        [SerializeField][Output] private GameObject obj;
        private int index;

        private void Awake()
        {
            objectPool = new List<GameObject>();
        }

        public override object GetValue(NodePort port)
        {
            return obj;
        }

        private GameObject Get()
        {
            if (index -1 > poolSize) index = 0;

            GameObject newObj;
            if ((objectPool.Count) < poolSize)
            {
                newObj = CreateNew();
                return newObj;
            }
            else
            {
                try
                {
                    newObj = objectPool[index];
                }
                catch (System.Exception)
                {
                    objectPool.RemoveAll(item => item == null);

                    if (objectPool.Count == 0)
                    {
                        newObj = CreateNew();
                        return newObj;
                    }
                    else
                    {
                        index = objectPool.IndexOf(objectPool.First());
                    }
                }

                newObj = objectPool[index];
                index++;
                return newObj;
            }

        }

        private GameObject CreateNew()
        {
            spawnScale = GetInputValue(nameof(spawnScale), spawnScale);

            GameObject createdObj = Instantiate<GameObject>(prefab, spawnPosition, spawnRotation);
            createdObj.transform.localScale = spawnScale;
            createdObj.name = $"{prefab.name} - {index}";

            if (parent != null) createdObj.transform.SetParent(parent);

            objectPool.Add(createdObj);
            index++;
            return createdObj;
        }

        private void GetGO()
        {
            objectPool = GetInputValue(nameof(objectPool), objectPool);
            prefab = GetInputValue(nameof(prefab), prefab);
            parent = GetInputValue(nameof(parent), parent);
            spawnPosition = GetInputValue(nameof(spawnPosition), spawnPosition);
            spawnRotation = GetInputValue(nameof(spawnRotation), spawnRotation);
            spawnScale = GetInputValue(nameof(spawnScale), spawnScale);
            poolSize = GetInputValue(nameof(poolSize), poolSize);

            obj = Get();
            if (obj == null) return;

            obj.SetActive(false);
            obj.transform.position = spawnPosition;
            obj.transform.rotation = spawnRotation;
            obj.transform.localScale = spawnScale;
            obj.SetActive(true);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            GetGO();
            base.ExecuteNode(exit);
        }
    }
}