using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler class for OutfitZones, attach script to GameObject in scene,
/// assign a Collider from each zone to the script, and assign an associated override value
/// to each collider.
/// </summary>
public class OutfitZoneHandler : MonoBehaviour
{
    //Public variables
    [Header("")]
    [Header("Add all outfit zones here")]
    [Header("")]
    public List<OutfitZone> outfitZones;
    [Header("")]
    [Header("Override value for if player isn't in a zone")]
    [Header("")]
    public int defaultZoneOverride;
    //private variables
    int startZoneOverride;

    int layermask;

    float outfitZoneCounter = 0;
    float checkPeriod = 0.5f;
    Collider[] areaColliders;

#if UNITY_ENGAGE
    /// <summary>
    /// Initialise lists and add values to dictionary
    /// </summary>
    private void Start()
    {
        listenersSet = false;
        layermask = 1 << 20;
        outfitZoneCounter = 0;
        startZoneOverride = TheaterVariables.roomOutfitOverrideOnStart;
    }

    /// <summary>
    /// Check if player summoned and start summon coroutine
    /// otherwise change outfit immediately
    /// </summary>
    void Update()
    {
        if (Time.timeSinceLevelLoad < 5f)
            return;

        if (outfitZoneCounter > 0)
        {
            outfitZoneCounter -= Time.deltaTime;
        }
        else
        {
            outfitZoneCounter = checkPeriod;
            DoZoneCheck();
        }        
    }

    void DoZoneCheck()
    {
        outfitZoneCounter = checkPeriod;

        if (ENG_IGM_PlayerManager.instance == null)
            return;

        if (ENG_IGM_PlayerManager.instance.myPlayerObject != null)
        {
            if (!listenersSet)
                AddListeners();

            areaColliders = Physics.OverlapSphere(ENG_IGM_PlayerManager.instance.myPlayerObject.playerObject.transform.position, 0.25f, layermask, QueryTriggerInteraction.Collide);
            for (int i = 0; i < areaColliders.Length; i++)
            {
                if (areaColliders[i].gameObject)
                {
                    OutfitZone zone = areaColliders[i].gameObject.GetComponent<OutfitZone>();
                    if (zone != null && outfitZones.Contains(zone))
                    {
                        if (zone.outfit_override != ENG_IGM_PlayerManager.instance.myPlayerObject.playerSyncScript.lvr_Avatar.avatarRef.outfitOverridesIndex)
                            ENG_IGM_PlayerManager.instance.myPlayerObject.playerSyncScript.lvr_Avatar.avatarRef.OutfitOverride(zone.outfit_override);

                        return;
                    }
                }
            }

            if (defaultZoneOverride != ENG_IGM_PlayerManager.instance.myPlayerObject.playerSyncScript.lvr_Avatar.avatarRef.outfitOverridesIndex)
                ENG_IGM_PlayerManager.instance.myPlayerObject.playerSyncScript.lvr_Avatar.avatarRef.OutfitOverride(defaultZoneOverride);

        }
    }

    bool listenersSet;
    void AddListeners()
    {
        listenersSet = true;
        ENG_IGM_PlayerManager.instance.OnPlayerSit.AddListener(DoZoneCheck);
        ENG_IGM_PlayerManager.instance.OnPlayerTeleported.AddListener(DoZoneCheck);
    }
#endif
}