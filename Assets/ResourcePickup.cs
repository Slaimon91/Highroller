using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour, IInteractable
{
    [SerializeField] bool HP = false;
    [SerializeField] bool gaia = false;
    [SerializeField] bool goldenAcorns = false;
    [SerializeField] int amount = 0;
    public bool persistentObject = false;
    public int nrOfPickups = 1;
    [HideInInspector] public string id;
    void Awake()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }
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
                gameObject.SetActive(false);
            }
        }
    }
    private void Save(string temp)
    {
        SaveData.current.resourcePickups.Add(new ResourcePickupData(gameObject.GetComponent<ResourcePickup>()));
    }

    public void Load(string temp)
    {
        ResourcePickupData data = SaveData.current.resourcePickups.Find(x => x.id == id);

        if (data != default)
        {
            nrOfPickups = data.nrOfPickups;
            persistentObject = data.persistentObject;

            if (!persistentObject && nrOfPickups == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}
[System.Serializable]
public class ResourcePickupData
{
    public string id;
    public int nrOfPickups;
    public bool persistentObject;

    public ResourcePickupData(ResourcePickup resourcePickup)
    {
        id = resourcePickup.id;
        nrOfPickups = resourcePickup.nrOfPickups;
        persistentObject = resourcePickup.persistentObject;
    }
}

