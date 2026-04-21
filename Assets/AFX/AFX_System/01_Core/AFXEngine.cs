using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engage.AFX.v1
{
    [AddComponentMenu("AFX/AFX Engine")]
    [RequireComponent(typeof(AFXObjectReferenceManager))]
    public class AFXEngine : MonoBehaviour
    {
        private const float engineVersion = 1.000f; // Update this with every major revision of system.
        public float EngineVersion { get => engineVersion; }

        private AFXObjectReferenceManager referenceManager;
        public AFXObjectReferenceManager ReferenceManager
        {
            get
            {
                if (referenceManager == null)
                {
                    referenceManager = GetComponent<AFXObjectReferenceManager>();
                    return referenceManager;
                }
                else
                {
                    return referenceManager;
                }
            }
        }

        [FormerlySerializedAs("AFXNodeGraph")]
        [SerializeField]
        private AFXGraph afxNodeGraph;
        public AFXGraph AFXNodeGraph { get => afxNodeGraph; }

        private AFXGraph afxNodeGraphRuntimeVersion;
        public AFXGraph AfxNodeGraphRuntimeVersion { get => afxNodeGraphRuntimeVersion; set => afxNodeGraphRuntimeVersion = value; }

        private int timesChecked;
        private bool ready;
        private bool skippedEnable;

        private void Awake()
        {
            AfxNodeGraphRuntimeVersion = AFXNodeGraph.Copy() as AFXGraph; // get an editable runtime version of the graph, instantiates all the nodes too.
        }

        private IEnumerator Start()
        {
            if (AfxNodeGraphRuntimeVersion == null) yield break;

            InjectObjectReferences();
            yield return new WaitUntil(() => !HasNullInstances());

            ready = true;

            if (skippedEnable)
            {
                AfxNodeGraphRuntimeVersion.AFXOnEnable?.Invoke();
            }

            AfxNodeGraphRuntimeVersion.AFXStart?.Invoke();
        }
        
        private void OnEnable()
        {
            if (!ready)
            {
                skippedEnable = true;
                return;
            }

            AfxNodeGraphRuntimeVersion.AFXOnEnable?.Invoke();
        }

        private void OnDisable()
        {
            AfxNodeGraphRuntimeVersion.AFXOnDisable?.Invoke();
        }

        private void Update()
        {
            AfxNodeGraphRuntimeVersion.AFXUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            AfxNodeGraphRuntimeVersion.AFXLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            AfxNodeGraphRuntimeVersion.AFXFixedUpdate?.Invoke();
        }

        private void InjectObjectReferences()
        {
            foreach (ObjectReferenceSlot objRef in ReferenceManager.ObjectRefsEngine)
            {
                AfxNodeGraphRuntimeVersion.ObjectReferencesGraph.Add(objRef.ReferenceName, objRef.ReferenceValue);
            }
        }

        private bool HasNullInstances()
        {
            RetryLimit();
            foreach (var objRef in AfxNodeGraphRuntimeVersion.ObjectReferencesGraph)
            {
                if (objRef.Value == null) return true;
            }

            foreach (IDelayUntilReady item in AfxNodeGraphRuntimeVersion.nodes.OfType<IDelayUntilReady>())
            {
                if (!item.IsValueReady()) return true;
            }

            return false;
        }

        private void RetryLimit()
        {
            timesChecked++;
            if (timesChecked > 1000)
            {
                StopAllCoroutines();
                Debug.Log($"[{this.name}] - Failed to load all necessary references");
            }
        }
    }
}