using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveOWReward : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] ItemPickup itemPickup;
    [SerializeField] int hpAmount;
    [SerializeField] int gaiaAmount;
    [SerializeField] int goldenAcornAmount;

    private void Awake()
    {
        StartCoroutine(LookForPlayer());
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LookForPlayer()
    {
        if((playerController = FindObjectOfType<PlayerController>()) == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(LookForPlayer());
        }
        else
        {
            if(itemPickup != null)
            {
                itemPickup.Interact();
            }
            else if(hpAmount != 0)
            {
                FindObjectOfType<LaunchRewards>().LanuchHPRewardbox(hpAmount);
            }
            else if(gaiaAmount != 0)
            {
                FindObjectOfType<LaunchRewards>().LanuchGaiaRewardbox(gaiaAmount);
            }
            else if(goldenAcornAmount != 0)
            {
                FindObjectOfType<LaunchRewards>().LanuchGARewardbox(goldenAcornAmount);
            }

            Destroy(gameObject);
        }
    }    
}
