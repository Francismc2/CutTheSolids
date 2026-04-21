using XNodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using XNode;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;

namespace Engage.AFX.v1
{
    [InitializeOnLoad]
    [CustomNodeGraphEditor(typeof(AFXGraph))]
    public class AFXNodeGraphEditor : NodeGraphEditor
    {
        private const float NoodlePulseSpeed = 0.8f;

        private Dictionary<Type, Type> nodeTypeMap = new Dictionary<Type, Type>();
        private AFXGraph afxGraph;
        private AFXEngine afxEngine;
        private AFXNodeSearchWindowProvider searchWindow;

        private readonly UnityEngine.Gradient gradient = new UnityEngine.Gradient();
        private readonly GradientColorKey[] gradientColorKeys = new GradientColorKey[1];
        private readonly GradientAlphaKey[] gradientAlphaKey = new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f) };

        public void Awake()
        {
            if (afxGraph == null)
            {
                afxGraph = (AFXGraph)target;
            }
        }

        public override void AddContextMenuItems(GenericMenu menu)
        {
            Dictionary<Type, string> nodeInfo = new Dictionary<Type, string>();
            Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);
            var nodeTypes = NodeEditorReflection.nodeTypes.OrderBy(type => GetNodeMenuOrder(type)).ToArray();
            for (int i = 0; i < nodeTypes.Length; i++)
            {
                Type type = nodeTypes[i];
                string path = GetNodeMenuName(type);
                if (string.IsNullOrEmpty(path)) continue;
                nodeInfo.Add(type, path);
            }
            if (searchWindow == null) searchWindow = (AFXNodeSearchWindowProvider)ScriptableObject.CreateInstance(typeof(AFXNodeSearchWindowProvider));
            searchWindow.Setup(nodeInfo, (AFXGraph)target, pos);
            SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), searchWindow);
        }

        public override NodeEditorPreferences.Settings GetDefaultPreferences()
        {
            try
            {
                string path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("AFX_Prefs.json")[0]);
                TextAsset prefsFile = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
                return JsonUtility.FromJson<NodeEditorPreferences.Settings>(prefsFile.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return base.GetDefaultPreferences();
            }
        }

        public override NoodleStroke GetNoodleStroke(NodePort output, NodePort input)
        {
            if (output.ValueType == typeof(AFXFlow))
            {
                return NoodleStroke.Dashed;
            }

            return base.GetNoodleStroke(output, input);
        }

        public override string GetPortTooltip(XNode.NodePort port)
        {
            System.Type portType = port.ValueType;
            string tooltip = "";
            tooltip = portType.PrettyName();
            if (!port.IsOutput) return tooltip;

            if (!Application.isPlaying)
            {
                if (port.ValueType == typeof(AFXFlow))
                {
                    return "Value preview unavailable outside of playmode";
                }

                if (port.GetInputValue() == null)
                {
                    return "Value preview unavailable outside of playmode";
                }

                if (port.node.GetType().IsSubclassOf(typeof(AFXActiveNode)))
                {
                    return "Value preview unavailable outside of playmode";
                }

                if (port.node.GetType().IsSubclassOf(typeof(ObjectReferenceNode)))
                {
                    return "Value preview unavailable outside of playmode";
                }
            }
            else
            {
                if (port.ValueType == typeof(AFXFlow))
                {
                    return "Value preview unavailable";
                }
            }

            object obj = port.node.GetValue(port);
            tooltip += " = " + (obj != null ? obj.ToString() : "null");
            return tooltip;
        }

        public override UnityEngine.Gradient GetNoodleGradient(NodePort output, NodePort input)
        {
            if (!Application.isPlaying) return base.GetNoodleGradient(output, input);

            if (output.ValueType == typeof(AFXFlow))
            {
                AFXSequentialNode node = output.node as AFXSequentialNode;
                AFXFlow flow = output.GetPortField() as AFXFlow;

                if (node == null || flow == null) return base.GetNoodleGradient(output, input);

                if (flow.ActiveNoodle)
                {
                    RefreshGradients(gradient, output, AFXNodeColors.noodleActiveGradient1, AFXNodeColors.noodleActiveGradient2);
                }
                else
                {
                    RefreshGradients(gradient, output, AFXNodeColors.noodleInactiveGradient, AFXNodeColors.noodleInactiveGradient);
                }

                return gradient;
            }

            return base.GetNoodleGradient(output, input);
        }

        private void RefreshGradients(UnityEngine.Gradient gradient, NodePort port, Color color1, Color color2)
        {
            gradientColorKeys[0].color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * NoodlePulseSpeed, 1f));
            gradient.SetKeys(gradientColorKeys, gradientAlphaKey);
        }

        public void CreateNode<T>(UnityEngine.Object selection, Vector2 position) where T: ObjectReferenceNode
        {
            T addedNode = (T)CreateNode(typeof(T), NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition));
            addedNode.SetReferenceName(selection.name);
            if (afxEngine == null) GetEngineReference();
            if (afxEngine != null)
            {
                Type selectionType = selection.GetType();
                addedNode.SetReferenceName(afxEngine.ReferenceManager.AddReferenceSlot(selection.name + "-" + selectionType.Name,selection, selectionType));
            }
        }

        public override void OnDropObjects(UnityEngine.Object[] objects)
        {
            //nodeTypeMap.Clear(); //for Debuging
            if (nodeTypeMap.Count<1)
            {
                nodeTypeMap = CreateNodeTypeMap();
            }

            foreach (UnityEngine.Object selection in objects)
            {
                Type selectionType = selection.GetType();

                ////DragAndDrop Auto Ref Nodes
                if (nodeTypeMap.ContainsKey(selectionType))
                {
                    Type nodeType = nodeTypeMap[selectionType];
                    ObjectReferenceNode addedNode = (ObjectReferenceNode)CreateNode(nodeType, NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition));
                    addedNode.SetReferenceName(selection.name);
                    if (afxEngine == null) GetEngineReference();
                    if (afxEngine != null)
                    {
                        addedNode.SetReferenceName(afxEngine.ReferenceManager.AddReferenceSlot(selection.name + "-" + selectionType.Name, selection, selectionType));
                        afxEngine.ReferenceManager.RefreshReferenceSlots();
                    }
                    return;
                }

                ////DragAndDrop copy graph contents
                if (selectionType == typeof(AFXGraph))
                {
                    AFXGraph droppedGraph = (AFXGraph)selection;
                    NodeEditorWindow.copyBuffer = droppedGraph.nodes.ToArray();
                    NodeEditorWindow.current.PasteNodes(NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition));
                    afxEngine.ReferenceManager.RefreshReferenceSlots();
                }

                ////DragAndDrop if Subgraph
                if (selectionType == typeof(AFXSubGraph))
                {
                    if (window.HoveredNode is SubGraphNode) return;

                    SubGraphNode addedNode = (SubGraphNode)CreateNode(typeof(SubGraphNode), NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition));
                    addedNode.SubGraph = selection as AFXSubGraph;
                }
            }
        }

        private Dictionary<Type, Type> CreateNodeTypeMap()
        {
            Dictionary<Type, Type> nodeTypeMap = new Dictionary<Type, Type>();

            // Get all types derived from ObjectReferenceNode
            Type objectReferenceNodeType = typeof(ObjectReferenceNode<>);
            Type[] derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == objectReferenceNodeType && !t.IsAbstract)
                .ToArray();

            // Add manual override mappings to the nodeTypeMap dictionary
            Dictionary<Type, Type> overrideMappings = GetOverrideMappings();
            foreach (KeyValuePair<Type, Type> kvp in overrideMappings)
            {
                nodeTypeMap[kvp.Key] = kvp.Value;
            }

            // Populate the remaining nodeTypeMap dictionary
            foreach (Type derivedType in derivedTypes)
            {
                Type nodeType = derivedType.BaseType.GetGenericArguments()[0];

                if (!overrideMappings.ContainsKey(nodeType) && !nodeTypeMap.ContainsKey(nodeType))
                {
                    nodeTypeMap[nodeType] = derivedType;
                }
            }
            return nodeTypeMap;
        }

        private Dictionary<Type, Type> GetOverrideMappings()
        {
            // Define and fill the manual override mappings
            Dictionary<Type, Type> overrideMappings = new Dictionary<Type, Type>();
            overrideMappings[typeof(BoxCollider)] = typeof(ColliderReference);
            overrideMappings[typeof(SphereCollider)] = typeof(ColliderReference);
            overrideMappings[typeof(CapsuleCollider)] = typeof(ColliderReference);
            overrideMappings[typeof(MeshCollider)] = typeof(ColliderReference);
            return overrideMappings;
        }

        public override void RemoveNode(XNode.Node node)
        {
            base.RemoveNode(node);
            if (node.GetType().IsSubclassOf(typeof(ObjectReferenceNode)))
            {
                if (afxEngine == null) GetEngineReference();
                if (afxEngine != null)
                {
                    afxEngine.ReferenceManager.RefreshReferenceSlots();
                }
            }
        }

        private void GetEngineReference()
        {
            if (afxEngine == null)
            {
                foreach (AFXEngine engine in UnityEngine.Object.FindObjectsByType<AFXEngine>(FindObjectsSortMode.None))
                {
                    if (engine.AFXNodeGraph == afxGraph || engine.AfxNodeGraphRuntimeVersion == afxGraph)
                    {
                        afxEngine = engine;
                        break;
                    }
                }
            }
        }

        public override void OnOpen()
        {
            base.OnOpen();
            this.window.titleContent.text = target.name;
        }
    }
}