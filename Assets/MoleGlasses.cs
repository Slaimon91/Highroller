using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoleGlasses : MonoBehaviour, IInteractable
{
    Animator animator;
    [SerializeField] ItemPickup itemPickup;
    [HideInInspector] public bool hasSpawnedBling = false;
    [HideInInspector] public bool hasGlasses = false;
    [HideInInspector] public bool goblinDefeated = false;
    [SerializeField] GameObject glassShine;
    [SerializeField] Tile normalGrassTile;
    [SerializeField] Tile glassesGrassTile;
    [SerializeField] Tilemap glassesTilemap;
    [SerializeField] Vector3Int tilePos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }

    public void Interact()
    {
        if (!hasSpawnedBling)
        {
            hasSpawnedBling = true;
            glassShine.SetActive(true);
            glassesTilemap.SetTile(tilePos, glassesGrassTile);
        }

        if(hasSpawnedBling && goblinDefeated && !hasGlasses) //check against inventory
        {
            hasGlasses = true;
            animator.SetBool("hasGlasses", true);
            GetComponent<DialogueTrigger>().onFinishedDialogueCallback += MoveMole;
        }
    }

    private void MoveMole()
    {
        transform.position = transform.position + new Vector3(-1f, 0, 0);
        GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.83f, 0.5f);
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback -= MoveMole;
        gameObject.GetComponent<DialogueTrigger>().AdvanceDialogue();
    }

    private void Save(string temp)
    {
        SaveData.current.moleGlasses = new MoleGlassesData(gameObject.GetComponent<MoleGlasses>());
    }

    public void Load(string temp)
    {
        MoleGlassesData data = SaveData.current.moleGlasses;

        if (data != default)
        {
            hasSpawnedBling = data.hasSpawnedBling;
            hasGlasses = data.hasGlasses;
        }

        if (!hasSpawnedBling || goblinDefeated)
        {
            glassShine.SetActive(false);
        }

        if (hasSpawnedBling && !hasGlasses && !goblinDefeated)
        {
            glassShine.SetActive(true);
            glassesTilemap.SetTile(tilePos, glassesGrassTile);
        }

        if (hasGlasses)
        {
            animator.SetBool("hasGlasses", true);
            transform.position = transform.position + new Vector3(-1, 0, 0);
            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.83f, 0.5f);
        }
    }

    public void GoblinDefeated()
    {
        glassShine.SetActive(false);
        glassesTilemap.SetTile(tilePos, normalGrassTile);
        goblinDefeated = true;
        gameObject.GetComponent<DialogueTrigger>().AdvanceDialogue();
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}
[System.Serializable]
public class MoleGlassesData
{
    public bool hasSpawnedBling;
    public bool hasGlasses;
    public bool goblinDefeated;

    public MoleGlassesData(MoleGlasses moleGlasses)
    {
        hasSpawnedBling = moleGlasses.hasSpawnedBling;
        hasGlasses = moleGlasses.hasGlasses;
        goblinDefeated = moleGlasses.goblinDefeated;
    }
}

