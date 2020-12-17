using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTreasure : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite beforeSprite;
    [SerializeField] Sprite afterSprite;
    [HideInInspector] public bool isTaken = false;
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
        if(FindObjectOfType<PlayerController>() != null)
        {
            if (FindObjectOfType<PlayerController>().tileFlipping && !isTaken)
            {
                FindObjectOfType<EncounterManager>().onWaitForFlipDoneCallback += WaitForAnimDone;
            }
        }
    }

    private void WaitForAnimDone()
    {
        isTaken = true;
        gameObject.layer = 11;
        GetComponentInParent<SpriteRenderer>().sprite = afterSprite;
        FindObjectOfType<EncounterManager>().onWaitForFlipDoneCallback -= WaitForAnimDone;
    }
    private void Save(string temp)
    {
        SaveData.current.tileTreasures.Add(new TileTreasureData(gameObject.GetComponent<TileTreasure>()));
    }

    public void Load(string temp)
    {
        TileTreasureData data = SaveData.current.tileTreasures.Find(x => x.id == id);

        if (data != default)
        {
            isTaken = data.isTaken;

            if (isTaken)
            {
                gameObject.layer = 11;
                GetComponentInParent<SpriteRenderer>().sprite = afterSprite;
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
public class TileTreasureData
{
    public string id;
    public bool isTaken;

    public TileTreasureData(TileTreasure tileTreasure)
    {
        id = tileTreasure.id;
        isTaken = tileTreasure.isTaken;
    }
}