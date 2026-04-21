
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Engage.Avatars.Poses;
// ToDo:
// Possibly turn off animations while seated?
// Set up reset view for wandering vr camera

public class PoseSelector : MonoBehaviour
{
    [SerializeField]
    public bool isInitialized = false;
    public bool allObjectsReady
    {
        get { return CheckAnchors(); }
    }
    ////////////////////////////////

    [HideInInspector]
    public Vector3 SitTriggerSeatPosition; // gotten from initalise is old  gen 1 sit triggers pelvis positioner

    ///////////////////////////////////////

    [SerializeField]
    public bool editMode;
    [SerializeField]
    public PoseDataAsset poseToEdit;
    ///////////////////////////////////////

    bool useOverridePoses = false;
    [SerializeField]
    bool allowRandomSafePose = true;
    [SerializeField]
    bool allowOpenLegPoses = true;
    [SerializeField]
    bool allowClosedLegPoses = true;
    /////////////////////////////////////
    public PoseDataAsset ChosenPose
    {
        get { return _chosenPose; }
        private set { _chosenPose = value; }
    }
    PoseDataAsset _chosenPose;

    PoseDataAsset generatedBackUpPose;

    [SerializeField]
    private List<PoseDataAsset> overridePoseList;
    [SerializeField]
    private List<PoseDataAsset> safePoses = new List<PoseDataAsset>();
    [SerializeField]
    private List<List<PoseDataAsset>> PoseTypesToUse = new List<List<PoseDataAsset>>();

    [SerializeField]
    public List<PoseDataAsset> openLegsPoses = new List<PoseDataAsset>();
    [SerializeField]
    public List<PoseDataAsset> closedLegsPoses = new List<PoseDataAsset>();
    ////////////////////////////////////

    [SerializeField]
    public float avatarHeight = 1.88f;

    #region Anchor Transforms

    [SerializeField]
    public GameObject rootPositioner;
    [SerializeField]
    public GameObject idealHeadPosition;
    [SerializeField]
    GameObject headOffsetFromIdeal;
    ///////////////////////////////
    [SerializeField]
    public Transform head_Anchor_Seat;
    [SerializeField]
    public Transform chest_Anchor_Seat;
    [SerializeField]
    public Transform pelvis_Anchor_Seat;
    [SerializeField]
    public Transform leftHand_Anchor_Seat;
    [SerializeField]
    public Transform rightHand_Anchor_Seat;
    [SerializeField]
    public Transform leftFoot_Anchor_Seat;
    [SerializeField]
    public Transform rightFoot_Anchor_Seat;
    [SerializeField]
    public Transform leftElbow_Anchor_Seat;
    [SerializeField]
    public Transform rightElbow_Anchor_Seat;
    [SerializeField]
    public Transform leftKnee_Anchor_Seat;
    [SerializeField]
    public Transform rightKnee_Anchor_Seat;

    #endregion
    //////////////////////
    float height_Cached;
    [SerializeField]
    bool allowOpenLegPoses_Cached;
    [SerializeField]
    bool allowClosedLegPoses_Cached;

    [SerializeField]
    bool isVRAndNotAway_Cached;
    [SerializeField]
    public float maxHeadDistance = 1.25f;

    ///////////////////////////////////


    bool CheckAnchors()
    {
        head_Anchor_Seat = CheckOrCreateAnchor("HEAD", head_Anchor_Seat);
        chest_Anchor_Seat = CheckOrCreateAnchor("CHEST", chest_Anchor_Seat);
        pelvis_Anchor_Seat = CheckOrCreateAnchor("PELVIS", pelvis_Anchor_Seat);
        leftHand_Anchor_Seat = CheckOrCreateAnchor("LEFT_HAND", leftHand_Anchor_Seat);
        rightHand_Anchor_Seat = CheckOrCreateAnchor("RIGHT_HAND", rightHand_Anchor_Seat);
        leftFoot_Anchor_Seat = CheckOrCreateAnchor("LEFT_FOOT", leftFoot_Anchor_Seat);
        rightFoot_Anchor_Seat = CheckOrCreateAnchor("RIGHT_FOOT", rightFoot_Anchor_Seat);
        leftElbow_Anchor_Seat = CheckOrCreateAnchor("LEFT_ELBOW", leftElbow_Anchor_Seat);
        rightElbow_Anchor_Seat = CheckOrCreateAnchor("RIGHT_ELBOW", rightElbow_Anchor_Seat);
        leftKnee_Anchor_Seat = CheckOrCreateAnchor("LEFT_KNEE", leftKnee_Anchor_Seat);
        rightKnee_Anchor_Seat = CheckOrCreateAnchor("RIGHT_KNEE", rightKnee_Anchor_Seat);
        rootPositioner = CheckOrCreateAnchor("rootPositioner", rootPositioner).gameObject;
        idealHeadPosition = CheckOrCreateAnchor("idealHeadPosition", idealHeadPosition).gameObject;
        headOffsetFromIdeal = CheckOrCreateAnchor("headOffsetFromIdeal", headOffsetFromIdeal).gameObject;

        if (idealHeadPosition.transform.parent != pelvis_Anchor_Seat.transform)
        {
            idealHeadPosition.transform.SetParent(pelvis_Anchor_Seat, false);
        }

        if (headOffsetFromIdeal.transform.parent != idealHeadPosition.transform)
        {
            headOffsetFromIdeal.transform.SetParent(idealHeadPosition.transform, false);
        }

        if (rootPositioner == null)
        {
            return false;
        }
        if (idealHeadPosition == null)
        {
            return false;
        }
        if (headOffsetFromIdeal == null)
        {
            return false;
        }
        if (head_Anchor_Seat == null)
        {
            return false;
        }
        if (chest_Anchor_Seat == null)
        {
            return false;
        }
        if (pelvis_Anchor_Seat == null)
        {
            return false;
        }
        if (leftHand_Anchor_Seat == null)
        {
            return false;
        }
        if (rightHand_Anchor_Seat == null)
        {
            return false;
        }
        if (leftFoot_Anchor_Seat == null)
        {
            return false;
        }
        if (rightFoot_Anchor_Seat == null)
        {
            return false;
        }
        if (leftElbow_Anchor_Seat == null)
        {
            return false;
        }
        if (rightElbow_Anchor_Seat == null)
        {
            return false;
        }
        if (leftKnee_Anchor_Seat == null)
        {
            return false;
        }
        if (rightKnee_Anchor_Seat == null)
        {
            return false;
        }
        if (pelvis_Anchor_Seat == null)
        {
            return false;
        }
        if (pelvis_Anchor_Seat == null)
        {
            return false;
        }
        return true;
    }

    Transform CheckOrCreateAnchor(string _name, GameObject checkObj)
    {
        return CheckOrCreateAnchor(_name, checkObj != null ? checkObj.transform : null);
    }
    Transform CheckOrCreateAnchor(string _name, Transform checkTrans)
    {
        if (checkTrans != null)
        {
            return checkTrans;
        }

        Transform newTrans = new GameObject().transform;
        newTrans.name = _name;
        newTrans.SetParent(transform, false);
        return newTrans;
    }




    public void OverrideWithNewPoses(List<PoseDataAsset> poseListIn)
    {
        overridePoseList = poseListIn;
    }
    public void OverrideWithNewPoses(bool activateOverride)
    {
        useOverridePoses = activateOverride;
        SeatReset();
    }
    /// <summary>
    /// Filters through all the attached poses for appropriot height  and allowed pose types (Garment tags can disallow pose types) 
    /// Creates a list of safe poses that can then be picked at random or by priority
    /// A special default pose will will need to be made to use when no safe pose can be found
    /// </summary>
    /// <param name="useOverridePoses">OverrideWithNewPoses</param>
    void CreateSafePoseList()
    {
        safePoses.Clear();
        PoseTypesToUse.Clear();
        if (useOverridePoses && overridePoseList != null)
        {
            PoseTypesToUse.Add(overridePoseList);
        }
        else
        {
            if (allowOpenLegPoses == true)
            {
                PoseTypesToUse.Add(openLegsPoses);
            }
            if (allowClosedLegPoses == true)
            {
                PoseTypesToUse.Add(closedLegsPoses);
            }
        }


        if (PoseTypesToUse.Count > 0)
        {
            foreach (List<PoseDataAsset> poseType in PoseTypesToUse)
            {
                foreach (PoseDataAsset pose in poseType)
                {
                    if (pose == null)
                    {
                        continue;
                    }
                    if (avatarHeight >= pose.heightRange.x && avatarHeight <= pose.heightRange.y)
                    {
                        safePoses.Add(pose);

                    }
                }
            }
        }


    }



    void Update() //Main Update Loop //Possibly can be run from seatManager so that each seat doesn't need to run until someone is seated.
    {



        if (height_Cached != avatarHeight) // we  need to swap out poses dynamically when player changes height or in future when they select a pose type from UI, but don't want to do this every frame if nothiong changed
        {
            SeatReset();
        }
        if (allowOpenLegPoses_Cached != allowOpenLegPoses)
        {
            SeatReset();
        }
        if (allowClosedLegPoses_Cached != allowClosedLegPoses)
        {
            SeatReset();
        }

        if (editMode)// if we're in edit mode we stop applying the pose instead we write the pose to the posedata Asset file
        {
            if (poseToEdit != null)
            {
                IdealHeadPositioner();
                PositionRoot();

                EditPoseData(poseToEdit);
            }
        }
        else //apply the pose
        {
            PositionRoot();
            if (!_chosenPose.flags.HasFlag(PoseDataAsset.poseFlags.ignoredPose))
            {
                GetPoseFromData(_chosenPose);
            }

            ApplyPoseToAvatar();
        }

    }


    public void SeatReset() // calcualte and set up all the stuff for if a player get's in seat or changes themselves (height etc)
    {
        generatedBackUpPose = null;
        //FilterPosesByClothesTags();
        IdealHeadPositioner();
        PositionRoot();
        CreateSafePoseList();
        if (safePoses.Count > 0)
        {
            if (allowRandomSafePose)
            {
                ChosenPose = safePoses[Random.Range(0, safePoses.Count)];
            }
            else
            {
                ChosenPose = safePoses[0];
            }
        }
        else
        {
            if (generatedBackUpPose == null)
            {
                generatedBackUpPose = CreateBackUpPose(SitTriggerSeatPosition);
            }
            ChosenPose = generatedBackUpPose;
        }
        height_Cached = avatarHeight;
        allowOpenLegPoses_Cached = allowOpenLegPoses;
        allowClosedLegPoses_Cached = allowClosedLegPoses;
    }


    PoseDataAsset CreateBackUpPose(Vector3 SitTriggerSeatPositionIN)
    {

        float AvatarScale = avatarHeight / 4;
        PoseDataAsset defaultPose = ScriptableObject.CreateInstance<PoseDataAsset>();

        defaultPose.name = "Default Pose";

        defaultPose.maxHeadDistance = 1.25f;
        defaultPose.heightRange.x = 0f;
        defaultPose.heightRange.y = 10f;


        defaultPose.m_pelvis_Anchor_Pos = SitTriggerSeatPositionIN;
        defaultPose.m_pelvis_Anchor_Rot = new Vector3(0, 0, 0);

        defaultPose.m_head_Anchor_Pos = idealHeadPosition.transform.InverseTransformPoint(idealHeadPosition.transform.position - new Vector3(0, 0.07f, 0) * AvatarScale / transform.lossyScale.x);
        defaultPose.m_head_Anchor_Rot = Vector3.zero;

        defaultPose.m_chest_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + ((Vector3.up + Vector3.forward) * AvatarScale);

        defaultPose.m_leftHand_Anchor_Pos = (defaultPose.m_pelvis_Anchor_Pos + new Vector3(-.5f * AvatarScale / transform.lossyScale.x, 0, 0));
        defaultPose.m_leftHand_Anchor_Rot = new Vector3(45, 15, -45);

        defaultPose.m_rightHand_Anchor_Pos = (defaultPose.m_pelvis_Anchor_Pos + new Vector3(.5f * AvatarScale / transform.lossyScale.x, 0, 0));
        defaultPose.m_rightHand_Anchor_Rot = new Vector3(45, 15, 45);

        defaultPose.m_leftFoot_Anchor_Pos = (defaultPose.m_pelvis_Anchor_Pos + new Vector3(-.15f * AvatarScale / transform.lossyScale.x, -1 * AvatarScale / transform.lossyScale.y, 1 * AvatarScale / transform.lossyScale.z));
        defaultPose.m_leftFoot_Anchor_Rot = Vector3.zero;

        defaultPose.m_rightFoot_Anchor_Pos = (defaultPose.m_pelvis_Anchor_Pos + new Vector3(.15f * AvatarScale / transform.lossyScale.x, -1 * AvatarScale / transform.lossyScale.y, 1 * AvatarScale / transform.lossyScale.z));
        defaultPose.m_rightFoot_Anchor_Rot = Vector3.zero;

        defaultPose.m_leftElbow_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + new Vector3(defaultPose.m_leftHand_Anchor_Pos.x, defaultPose.m_pelvis_Anchor_Pos.y * AvatarScale, -1 * AvatarScale);
        defaultPose.m_rightElbow_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + new Vector3(defaultPose.m_rightHand_Anchor_Pos.x, defaultPose.m_pelvis_Anchor_Pos.y * AvatarScale, -1 * AvatarScale);


        defaultPose.m_leftKnee_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + new Vector3(defaultPose.m_leftFoot_Anchor_Pos.x, 1f * AvatarScale, defaultPose.m_leftFoot_Anchor_Pos.z + 0.1f * AvatarScale);
        defaultPose.m_rightKnee_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + new Vector3(defaultPose.m_rightFoot_Anchor_Pos.x, 1f * AvatarScale, defaultPose.m_rightFoot_Anchor_Pos.z + 0.1f * AvatarScale);

        if (defaultPose.m_leftFoot_Anchor_Pos.y < 0.23f * AvatarScale / transform.lossyScale.y)
        {
            defaultPose.m_leftFoot_Anchor_Pos.y = 0.23f * AvatarScale / transform.lossyScale.y;
            defaultPose.m_leftKnee_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + new Vector3(defaultPose.m_leftFoot_Anchor_Pos.x, 1f * AvatarScale, defaultPose.m_leftFoot_Anchor_Pos.z + 0.1f * AvatarScale);
        }
        if (defaultPose.m_rightFoot_Anchor_Pos.y < 0.23f * AvatarScale / transform.lossyScale.y)
        {
            defaultPose.m_rightFoot_Anchor_Pos.y = 0.23f * AvatarScale / transform.lossyScale.y;
            defaultPose.m_rightKnee_Anchor_Pos = defaultPose.m_pelvis_Anchor_Pos + new Vector3(defaultPose.m_rightFoot_Anchor_Pos.x, 1f * AvatarScale, defaultPose.m_rightFoot_Anchor_Pos.z + 0.1f * AvatarScale);
        }
        return defaultPose;
    }
    void EditPoseData(PoseDataAsset PoseDataIN) // write seat anchor pos/Rot to PoseDataAsset
    {
        if (headOffsetFromIdeal == null)
        {
            headOffsetFromIdeal = new GameObject("headOffsetFromIdeal");
            headOffsetFromIdeal.transform.SetParent(idealHeadPosition.transform);
        }

        headOffsetFromIdeal.transform.position = head_Anchor_Seat.transform.position;
        PoseDataIN.m_head_Anchor_Pos = headOffsetFromIdeal.transform.localPosition;


        PoseDataIN.m_chest_Anchor_Pos = chest_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_pelvis_Anchor_Pos = pelvis_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_leftHand_Anchor_Pos = leftHand_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_rightHand_Anchor_Pos = rightHand_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_leftFoot_Anchor_Pos = leftFoot_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_rightFoot_Anchor_Pos = rightFoot_Anchor_Seat.transform.localPosition;

        PoseDataIN.m_head_Anchor_Rot = head_Anchor_Seat.transform.localEulerAngles;
        PoseDataIN.m_chest_Anchor_Rot = chest_Anchor_Seat.transform.localEulerAngles;
        PoseDataIN.m_pelvis_Anchor_Rot = pelvis_Anchor_Seat.transform.localEulerAngles;
        PoseDataIN.m_leftHand_Anchor_Rot = leftHand_Anchor_Seat.transform.localEulerAngles;
        PoseDataIN.m_rightHand_Anchor_Rot = rightHand_Anchor_Seat.transform.localEulerAngles;
        PoseDataIN.m_leftFoot_Anchor_Rot = leftFoot_Anchor_Seat.transform.localEulerAngles;
        PoseDataIN.m_rightFoot_Anchor_Rot = rightFoot_Anchor_Seat.transform.localEulerAngles;

        PoseDataIN.m_rightKnee_Anchor_Pos = rightKnee_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_leftKnee_Anchor_Pos = leftKnee_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_rightElbow_Anchor_Pos = rightElbow_Anchor_Seat.transform.localPosition;
        PoseDataIN.m_leftElbow_Anchor_Pos = leftElbow_Anchor_Seat.transform.localPosition;
        
    }
    void GetPoseFromData(PoseDataAsset PoseDataIN) // move seat anchors to match PoseDataAsset
    {

        maxHeadDistance = PoseDataIN.maxHeadDistance;
        

        if (headOffsetFromIdeal == null)
        {
            headOffsetFromIdeal = new GameObject("headOffsetFromIdeal");
            if (idealHeadPosition != null)
            {
                headOffsetFromIdeal.transform.SetParent(idealHeadPosition.transform);
            }
            else
            {
                IdealHeadPositioner();
                headOffsetFromIdeal.transform.SetParent(idealHeadPosition.transform);
            }

        }
        headOffsetFromIdeal.transform.localPosition = PoseDataIN.m_head_Anchor_Pos;
        head_Anchor_Seat.transform.position = headOffsetFromIdeal.transform.position;


        chest_Anchor_Seat.transform.localPosition = PoseDataIN.m_chest_Anchor_Pos;
        pelvis_Anchor_Seat.transform.localPosition = PoseDataIN.m_pelvis_Anchor_Pos;
        leftHand_Anchor_Seat.transform.localPosition = PoseDataIN.m_leftHand_Anchor_Pos;
        rightHand_Anchor_Seat.transform.localPosition = PoseDataIN.m_rightHand_Anchor_Pos;
        leftFoot_Anchor_Seat.transform.localPosition = PoseDataIN.m_leftFoot_Anchor_Pos;
        rightFoot_Anchor_Seat.transform.localPosition = PoseDataIN.m_rightFoot_Anchor_Pos;

        head_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_head_Anchor_Rot;
        chest_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_chest_Anchor_Rot;
        pelvis_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_pelvis_Anchor_Rot;
        leftHand_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_leftHand_Anchor_Rot;
        rightHand_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_rightHand_Anchor_Rot;
        leftFoot_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_leftFoot_Anchor_Rot;
        rightFoot_Anchor_Seat.transform.localEulerAngles = PoseDataIN.m_rightFoot_Anchor_Rot;

        leftElbow_Anchor_Seat.transform.localPosition = PoseDataIN.m_leftElbow_Anchor_Pos;
        rightElbow_Anchor_Seat.transform.localPosition = PoseDataIN.m_rightElbow_Anchor_Pos;
        leftKnee_Anchor_Seat.transform.localPosition = PoseDataIN.m_leftKnee_Anchor_Pos;
        rightKnee_Anchor_Seat.transform.localPosition = PoseDataIN.m_rightKnee_Anchor_Pos;
    }
    void ApplyPoseToAvatar() // Move Avatar To match seat Anchors. 
    {



    }
    ////////////////////////////
    void PositionRoot() // create an object than can be refrenced to get the proper placment for the avatar // If the player avatar always matches this it should fix inconsistencies of bone rotations from VRIK
    {
        if (rootPositioner == null)
        {
            rootPositioner = new GameObject("RootPositioner");
            rootPositioner.transform.SetParent(this.gameObject.transform, false);
        }

        //Avatar is actually offset from floor, which is where 0.034 is coming from
        Vector3 newRootPos = new Vector3(pelvis_Anchor_Seat.transform.localPosition.x, 0.034f / transform.lossyScale.y, pelvis_Anchor_Seat.transform.localPosition.z);
        rootPositioner.transform.localPosition = newRootPos;
    }
    void IdealHeadPositioner()
    {
        if (idealHeadPosition == null)
        {
            idealHeadPosition = new GameObject("IdealHeadPosition");
        }
        idealHeadPosition.transform.SetParent(pelvis_Anchor_Seat, false);
        idealHeadPosition.transform.localPosition = new Vector3(0, (avatarHeight * 0.365f) / pelvis_Anchor_Seat.transform.lossyScale.y, 0);
    }

#if UNITY_EDITOR

    void OnEnable()
    {
        if (editMode)
        {
            if (poseToEdit != null)
            {
                avatarHeight = (poseToEdit.heightRange.x + poseToEdit.heightRange.y) / 2;
            }

        }
        //else
        //{
        //    if (safeposes[0] !=null)
        //    {
        //        avatarheight = (safeposes[0].heightrange.x + safeposes[0].heightrange.y) / 2;
        //    }
        //}

    }
    void Start()
    {
        ToggleEditModeOffWhenBuilding();
        SitTriggerSeatPosition = gameObject.GetComponent<LVR_SitTrigger>().m_seatPosition;

        EditorApplication.playModeStateChanged += UpdatePoseDataMetaFiles;
        CheckAnchors();
        SeatReset();
        if (editMode)
        {
            if (poseToEdit != null)
            {
                GetPoseFromData(poseToEdit);
                GetPoseFromData(poseToEdit);
                GetPoseFromData(poseToEdit);
            }
        }


    }
    private void UpdatePoseDataMetaFiles(PlayModeStateChange state) // when leaving play mode Unity does not save changes made to posedata assets to disk, this forces it to save changes to disk
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            IEnumerable<string> paths = new string[] { AssetDatabase.GetAssetPath(poseToEdit) };
            AssetDatabase.ForceReserializeAssets(paths);
        }

    }
    void ToggleEditModeOffWhenBuilding()
    {
        if (BuildPipeline.isBuildingPlayer)
        {
            editMode = false;
        }

    }

    public string currentPoseDisplay;
    private void OnDrawGizmos()
    {
        if (isInitialized && ChosenPose != null)
        {
            currentPoseDisplay = ChosenPose.name;
        }

        if (pelvis_Anchor_Seat != null)
        {
            DrawSphere(pelvis_Anchor_Seat.transform.position, Color.magenta, 0.09f);
        }
        if (head_Anchor_Seat != null)
        {
            DrawSphere(head_Anchor_Seat.transform.position, Color.green, 0.08f);
        }
        if (chest_Anchor_Seat != null)
        {
            DrawSphere(chest_Anchor_Seat.transform.position, Color.blue, 0.09f);
        }
        if (leftHand_Anchor_Seat != null)
        {
            DrawSphere(leftHand_Anchor_Seat.transform.position, Color.red);
        }
        if (rightHand_Anchor_Seat != null)
        {
            DrawSphere(rightHand_Anchor_Seat.transform.position, Color.red);
        }
        if (leftFoot_Anchor_Seat != null)
        {
            DrawSphere(leftFoot_Anchor_Seat.transform.position, Color.red);
        }
        if (rightFoot_Anchor_Seat != null)
        {
            DrawSphere(rightFoot_Anchor_Seat.transform.position, Color.red);
        }
        if (leftElbow_Anchor_Seat != null)
        {
            DrawSphere(leftElbow_Anchor_Seat.transform.position, Color.yellow);
        }
        if (rightElbow_Anchor_Seat != null)
        {
            DrawSphere(rightElbow_Anchor_Seat.transform.position, Color.yellow);
        }
        if (leftKnee_Anchor_Seat != null)
        {
            DrawSphere(leftKnee_Anchor_Seat.transform.position, Color.yellow);
        }
        if (rightKnee_Anchor_Seat != null)
        {
            DrawSphere(rightKnee_Anchor_Seat.transform.position, Color.yellow);
        }
    }
    private void DrawSphere(Vector3 pos, Color colour, float size = .06f)
    {
        Gizmos.color = colour;

        Gizmos.DrawSphere(pos, size);

        Gizmos.color = Color.white;
    }
#endif
}
