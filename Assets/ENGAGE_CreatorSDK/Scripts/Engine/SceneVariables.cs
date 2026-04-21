using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
/// <summary>
/// For manually defining a scene's unique variables
/// On an initial scene load (not additive), this script will create
/// a TheaterVariables instance and transfer these variables to it as 
/// static variables, and rules for the scene. Also sets up the scene
/// by instantiating the core GameObjects on MasterClient 
/// </summary>
public class SceneVariables : MonoBehaviour
{

    public enum LegacyWhiteboardType
    {
        none,
        blackboard,
        whiteboard
    }
    /// <summary>
    /// Actual scene name (ID)
    /// </summary>
	public string sceneFilename;

    /// <summary>The scale the scene was created at (Initial Engage scenes were 1.3)</summary>
    /// <remarks>Affects player height and vr scale settings</remarks>
	public float theSceneScale = 1f;

    /// <summary>
    /// The gravity of the scene
    /// </summary>
	public Vector3 gravity = new Vector3(0, -9.8f, 0);

    /// <summary>
    /// Empty Transform to identify the spawn point
    /// </summary>
    public GameObject userSpawnPoint;

    /// <summary>
    /// Numerical value identifying the radius (in meters) from spawn point the users can safely spawn.
    /// </summary>
    public float userSpawnRadius = 1;

    /// <summary>
    /// Disable local player shadows
    /// </summary>
    public bool localPlayerShadowsOff;

    /// <summary>
    /// Disable remote player shadows
    /// </summary>
    public bool remotePlayerShadowsOff;

    /// <summary>
    /// Velocity of the teleport function
    /// </summary>
    public float teleportVelocity = 12;

    //If there is a legacy whiteboard in this location, what type is it
    public LegacyWhiteboardType legacyWhiteboardType;

    /// <summary>Should spatial audio be longer distance</summary>
    /// <remarks>For outdoor scenes, try 1.5, otherwise typically 1</remarks>
    [Header("")]
    [Header("Voice FallOff Distance multiplier (1 = 15 meters)")]
    [Header("")]
    public float spatialAudioMultiplier = 1f;

    [Header("")]
    [Header("Perfect Seats / Summon to seat system (optional)")]
    [Header("")]
    // <summary>
    /// Players will not spawn into PerfectSeats, false by default
    /// </summary>
    public bool ignorePerfectSeatsOnSpawn = false;

    /// <summary>
    /// To use the perfect seat system, identify the seats in order here (optional)
    /// </summary>
    public List<LVR_SitTrigger> perfectSeatList = new List<LVR_SitTrigger>();

    [Header("")]
    [Header("UHD Video Default - for 360 video rooms, can cause issues with performance")]
    [Header("")]
    /// <summary>
    /// UHD Video Default - can cause issues with performance, useful for 360 video rooms
    /// </summary>
    public bool useUHDVideoAsDefault = false;

    [Header("")]
    [Header("Only if override is necessary, otherwise -1")]
    [Header("")]
    /// <summary>
    /// Outfit override for this scene (Or -1 for normal clothes)
    /// </summary>
    public int outfitOverrideOnStart = -1;


    [Header("")]
    [Header("Camera Clipping - set to false unless absolutely needed")]
    [Header("")]
    /// <summary>
    /// Override camera's near clip plane?
    /// </summary>
	public bool overrideMinDrawDistance = false;

    /// <summary>
    /// Override camera's far clip plane?
    /// </summary>
	public bool overrideMaxDrawDistance = false;

    /// <summary>
    /// Override amount for camera's near clip plane
    /// </summary>
	public float minDrawDistanceAmount = 0;

    /// <summary>
    /// Override amount for camera's far clip plane
    /// </summary>
	public float maxDrawDistanceAmount = 0;

    [Header("")]
    [Header("-----If this is not a locked, lesson-only scene, set the below to false!-----")]
    [Header("-----Be Very Careful with the below settings-----")]
    [Header("")]

    /// <summary>
    /// Keep headbox on, must manually remove via script or effect (careful with this)
    /// </summary>
    public bool keepHeadboxOnAfterStart = false;

    /// <summary>
    /// A safer way to delay headbox removal (specified seconds)
    /// </summary>
	public bool delayHeadBoxRemoval = false;

    /// <summary>
    /// Actual seconds to delay headbox removal
    /// </summary>
	public float headboxDelaySeconds = 0f;

    /// <summary>
    /// Disable IFX & recordings
    /// </summary>
    /// <remarks>Typically used for scenes that aren't free roam / part of an experience</remarks>
	public bool disableTablet = false;


    GameObject theaterVariablesObject = null;

}
