using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StartPositionCircle : MonoBehaviour
{
    public GameObject sceneVariablesObj;
    private SceneVariables m_sceneVariables;
    private float m_circleDefaultScale = 0.2f;
    private float m_radius;

   void OnValidate()
    {
        if (sceneVariablesObj.GetComponent<SceneVariables>() != null)
        {
            m_sceneVariables = sceneVariablesObj.GetComponent<SceneVariables>();
            Debug.Log("Scene Variables successfully assigned to StartPositionCircle");
        }
    }

    void Update()
    {
        m_radius = m_sceneVariables.userSpawnRadius;
        transform.localScale = new Vector3(m_circleDefaultScale * m_radius, 1, m_circleDefaultScale * m_radius);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.parent.transform.localScale = new Vector3(1, 1, 1);
    }
}
