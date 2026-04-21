namespace Engage.AFX.v1
{
    public static class AFXMenuTree
    {
        //Top Level---
        public const string Reference = "Reference/";
        public const string Flow = "Flow/";
        public const string Subgraph = "Subgraph/";
        public const string Variable = "Variable/";
        public const string Engage = "Engage/";
        public const string Events = "Events/";
        public const string Unity = "Unity/";
        public const string EngageTeam = "EngageTeamOnly/";

        //EngageTeamOnly---


        //Unity----
        //Debug
        public const string Debug = Unity + "Debug/";
        //Time
        public const string Time = Unity+"Time/";
        //Transform
        public const string Transform = Unity+"Transform/";
        public const string TransformGet = Transform + "Get/";
        public const string TransformSet = Transform + "Set/";
        public const string TransformSpaceSwitching = Transform + "SpaceSwitching/";
        //Physics 
        public const string Physics = Unity+ "Physics/";
        public const string Raycast = Physics + "Raycast/";
        public const string LayerMask = Raycast + "LayerMask/";
        public const string WheelCollider = Physics + "WheelCollider/";
        public const string PhysicsCollider = Physics + "Collider/";
        public const string PhysicsRigidBody = Physics + "RigidBody/";
        public const string PhysicsRigidBodyJoint = PhysicsRigidBody + "Joint/";
        public const string PhysicsRigidBodyJointSpring = PhysicsRigidBodyJoint + "Spring/";
        public const string PhysicsRigidBodyJointHinge = PhysicsRigidBodyJoint + "Hinge/";
        //Networking 
        public const string Networking = Engage+ "Networking/";
        public const string NetworkStateModules = Networking + "NetworkStateModules/";
        //Input 
        public const string Input = Unity + "Input/";
        public const string InputMouse = Input + "Mouse/";
        //GameObject 
        public const string GameObject = Unity + "GameObject/";
        public const string Instantiate = GameObject + "Instantiate/";
        //Invoke Events 
        public const string EventsInvoke = Events + "Invoke/";

        //Unity Events 
        public const string EventsUnity = Events + "Unity/";
        public const string EventsCollision = Events + "Collision/";

        //Unity Components--
        public const string Component = Unity + "Component/";
        public const string ComponentUI = Component + "UI/";
        public const string ComponentUITextMeshPro = ComponentUI + "TextMeshPro/";
        public const string ComponentConstraint = Unity + "Constraint/";
        public const string ComponentCollider = Component + "Collider/";
        public const string ComponentAnimation = Component + "Animation/";
        public const string ComponentAnimationSetParams = ComponentAnimation + "Set Paramaters/";

        public const string ComponentAudio = Component + "Audio/";
        public const string ComponentAudioSource = ComponentAudio + "AudioSource/";
        public const string ComponentAudioReverbZone = ComponentAudio + "AudioReverbZone/";

        public const string ComponentFX = Component + "FX/";
        public const string ComponentFXParticle = ComponentFX + "Particle/";

        public const string ComponentRendering = Component + "Rendering/";
        public const string ComponentRenderingCamera = ComponentRendering + "Camera/";
        public const string ComponentRenderingLight = ComponentRendering + "Light/";
        public const string ComponentRenderingLightSet = ComponentRenderingLight + "Set/";
        public const string ComponentRenderingMaterial = ComponentRendering + "Material/";
        public const string ComponentRenderingMaterialSet = ComponentRenderingMaterial + "Set/";
        
        //Engage Tree----
        public const string AFXCompanionScripts = Engage + "AFXCompanion/";
        public const string ConstantCollision = AFXCompanionScripts + "ConstantCollision/";
        public const string GrabObject = AFXCompanionScripts + "GrabObject/";
        public const string PathData = AFXCompanionScripts + "PathData/";
        public const string Player = Engage + "Player/";
        public const string Seat = Engage + "Seat/";
        public const string EngagePhysics = Engage + "Physics/";

        //Reference----
        public const string RefEngage = Reference +"Engage/";
        public const string RefAFXCompanionScripts = RefEngage + "AFXCompanion/";
        //GrabObject 
        public const string GrabObjectSystem = RefAFXCompanionScripts + "GrabObjectSystem/";

        //Unity Ref
        public const string RefUnity = Reference + "Unity/";
        public const string RefUnityUI = RefUnity + "UI/";
        public const string RefUnityConstraint = RefUnity + "Constraint/";
        public const string RefUnityAnimation = RefUnity + "Animation/";
        public const string RefUnityAudio = RefUnity + "Audio/";
        public const string RefUnityFX = RefUnity + "FX/";
        public const string RefUnityPhysics = RefUnity + "Physics/";
        public const string RefUnityPhysicsJoint = RefUnityPhysics + "Joint/";
        public const string RefUnityRendering = RefUnity + "Rendering/";

        //Variable Ref
        public const string RefVariable = Reference + "Variable/";
        public const string RefVariableList = RefVariable + "List/";

        //Variable Tree----
        //Comparison
        public const string Comparison = Variable + "Comparison/";
        //Math
        public const string Math = Variable + "Math/";
        public const string MathF = Math + "MathF/";
        //Bool
        public const string Bool = Variable + "Bool/";
        public const string BoolCast = Bool + "Cast/";
        public const string BoolLogic = Bool + "Logic/";
        //Float
        public const string Float = Variable + "Float/";
        public const string FloatCast = Float + "Cast/";
        public const string FloatLogic = Float + "Logic/";
        public const string FloatMath = Float + "Math/";
        public const string FloatMathF = FloatMath + "MathF/";
        //Int
        public const string Int = Variable + "Int/";
        public const string IntCast = Int + "Cast/";
        public const string IntLogic = Int + "Logic/";
        public const string IntMath = Int + "Math/";
        public const string IntMathF = IntMath + "MathF/";
        //String
        public const string String = Variable + "String/";
        public const string StringCast = String + "Cast/";
        public const string StringLogic = String + "Logic/";
        //Text
        public const string Text = Variable + "Text/";
        //Lists
        public const string List = Variable + "List/";
        //ListGameObject
        public const string ListsGamobject = List + "GameObject/";
        public const string ListsGamobjectFunctions = ListsGamobject + "Functions/";
        //ListPathData
        public const string ListsPathData = List + "PathData/";
        public const string ListsPathDataFunctions = ListsPathData + "Functions/";
        //UnityComponent Var
        public const string UnityComponentVar = Variable + "UnityComponent/";
        public const string UnityCompAnimationCurve = UnityComponentVar + "AnimationCurve/";
        public const string UnityCompAudio = UnityComponentVar + "Audio/";
        public const string UnityCompRendering = UnityComponentVar + "Rendering/";
        //Vector
        public const string Vector = Variable + "Vector/";
        public const string Vector2 = Vector + "Vector2/";
        public const string Vector2Math = Vector2 + "Math/";
        public const string Vector3 = Vector + "Vector3/";
        public const string Vector3Cast = Vector3 + "Cast/";
        public const string Vector3Math = Vector3 + "Math/";
        public const string Quaternion = Vector + "Quaternion/";
        public const string QuaternionCast = Quaternion + "Cast/";
        public const string QuaternionMath = Quaternion + "Math/";
    }
}