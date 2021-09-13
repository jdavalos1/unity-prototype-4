using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public PowerupTypes currentPowerup;
    public GameObject homingRocketsIndicator;
    public GameObject bounceIndicator;
    public GameObject poundIndicator;

    private float powerupTimer = 7.0f;
    private Coroutine timerCoroutine;
    
    public void TurnOnPowerup(PowerupTypes pT)
    {
        HandleStoppingCoroutine();
        switch (pT)
        {
            case PowerupTypes.Bounce:
                currentPowerup = PowerupTypes.Bounce;
                bounceIndicator.SetActive(true);
                StartCoroutine(PowerupTimer(bounceIndicator));
                break;
            case PowerupTypes.Rockets:
                currentPowerup = PowerupTypes.Rockets;
                homingRocketsIndicator.SetActive(true);
                StartCoroutine(PowerupTimer(homingRocketsIndicator));
                break;
            case PowerupTypes.Pound:
                currentPowerup = PowerupTypes.Pound;
                poundIndicator.SetActive(true);
                StartCoroutine(PowerupTimer(poundIndicator));
                break;
            default:
                break;
        }
    }

    void HandleStoppingCoroutine()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            bounceIndicator.SetActive(false);
            homingRocketsIndicator.SetActive(false);
        }
    }

    IEnumerator PowerupTimer(GameObject indicator)
    {
        yield return new WaitForSeconds(powerupTimer);
        indicator.SetActive(false);
        currentPowerup = PowerupTypes.None;
    }

    public enum PowerupTypes
    {
        None,
        Bounce,
        Rockets,
        Pound,
    }
}
