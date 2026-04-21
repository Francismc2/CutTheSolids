using UnityEngine;
using LSS;

[CreateAssetMenu(fileName = "Lightmap Data Object", menuName = "Lightmap/Lightmap Data Object", order = 61)]
public class LightmapDataSO : ScriptableObject{
    public string lightmap_id = "";
    public TextAsset lightmap_config;
    public Texture2D[]  lightmap_color;
    public Texture2D[]  lightmap_dir;
    public Texture2D[]  lightmap_shadowmask;

    LSS_Models.LightingScenarioModel lightingScenariosData = null;


    public LSS_Models.LightingScenarioModel GetJsonFile (bool forced = false) {
        if(lightmap_config == null || string.IsNullOrEmpty(lightmap_config.text) ) return null;
        else    return JsonUtility.FromJson<LSS_Models.LightingScenarioModel> (lightmap_config.text);
    }

    public LightmapData[] GetLightmaps(){
        LSS_Models.LightingScenarioModel lightingScenariosData = GetJsonFile ();

        if(lightingScenariosData != null){
			var newLightmaps = new LightmapData[lightingScenariosData.lightmaps.Length];
			for (int i = 0; i < newLightmaps.Length; i++) {
                newLightmaps[i] = new LightmapData();
                newLightmaps[i].lightmapColor = lightmap_color[i];
                // Debug.LogError("<color=green>Loaded color lightmap texture ("+i+")</color>");
                if (lightingScenariosData.lightmapsMode != LightmapsMode.NonDirectional) {
                    if (lightingScenariosData.lightmapsDir.Length > i && lightingScenariosData.lightmapsDir [i] != null) { // If the textuer existed and was set in the data file.
                        // Debug.LogError("<color=cyan>Loaded dir lightmap texture ("+i+")</color>");
                        newLightmaps[i].lightmapDir = lightmap_dir[i];
                    }
                    if (lightingScenariosData.lightmapsShadow.Length > i && lightingScenariosData.lightmapsShadow [i] != null) { // If the textuer existed and was set in the data file.
                        // Debug.LogError("<color=yellow>Loaded shadowmask lightmap texture ("+i+")</color>");
                        newLightmaps[i].shadowMask = lightmap_shadowmask[i];
                    }
                }
            }
            return newLightmaps;
        }
        Debug.LogError("ERROR No Lightmap texture loaded data");


        return null;
    }
}
