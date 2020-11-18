using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour, IInteractable
{
    [SerializeField] bool HP = false;
    [SerializeField] bool gaia = false;
    [SerializeField] bool goldenAcorns = false;
    [SerializeField] int amount = 0;
    [SerializeField] bool persistentObject = false;
    [SerializeField] int nrOfPickups = 1;
    public void Interact()
    {
        if(nrOfPickups > 0)
        {
            if (HP)
            {
                FindObjectOfType<LaunchRewards>().LanuchHPRewardbox(amount);
            }
            else if (gaia)
            {
                FindObjectOfType<LaunchRewards>().LanuchGaiaRewardbox(amount);
            }
            else if (goldenAcorns)
            {
                FindObjectOfType<LaunchRewards>().LanuchGARewardbox(amount);
            }

            nrOfPickups--;

            if (!persistentObject && nrOfPickups == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
