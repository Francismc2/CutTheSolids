using UnityEngine;

public class OnEnableDeactivateAfterSeconds : MonoBehaviour {
  
    public float secondsUntilDeactivate = 3;
    float countdownTimer = 0;

    private void OnEnable()
    {
        countdownTimer = secondsUntilDeactivate;
    }

    private void Update()
    {
        if (countdownTimer > 0)
            countdownTimer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }
}
