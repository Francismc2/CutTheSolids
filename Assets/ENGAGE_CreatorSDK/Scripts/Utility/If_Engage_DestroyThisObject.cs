using UnityEngine;

public class If_Engage_DestroyThisObject : MonoBehaviour
{
#if UNITY_ENGAGE
    private void Awake()
    {
    Destroy(gameObject);
    }
#endif
}
