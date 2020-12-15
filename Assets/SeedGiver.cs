using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGiver : MonoBehaviour, IInteractable
{
    Animator animator;
    [SerializeField] ItemPickup itemPickup;
    [HideInInspector] public bool hasGivenSeed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }

    private void GiveSeed()
    {
        itemPickup.Interact();
        FindObjectOfType<PlayerController>().onFinishedInteractingCallback -= GiveSeed;
    }

    public void Interact()
    {
        if(!hasGivenSeed)
        {
            FindObjectOfType<PlayerController>().onFinishedInteractingCallback += GiveSeed;
            hasGivenSeed = true;
        }

        else //prepare for separate dialogue after seed is given
        {

        }
    }
    private void Save(string temp)
    {
        SaveData.current.seedGiver = new SeedGiverData(gameObject.GetComponent<SeedGiver>());
    }

    public void Load(string temp)
    {
        SeedGiverData data = SaveData.current.seedGiver;

        if (data != default)
        {
            hasGivenSeed = data.hasGivenSeed;
        }
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}
[System.Serializable]
public class SeedGiverData
{
    public bool hasGivenSeed;

    public SeedGiverData(SeedGiver seedGiver)
    {
        hasGivenSeed = seedGiver.hasGivenSeed;
    }
}

