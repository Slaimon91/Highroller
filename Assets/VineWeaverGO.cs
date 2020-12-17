using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineWeaverGO : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] GameObject monsterIconVisualGO;
    [SerializeField] Sprite background;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public bool triggered = false;
    [HideInInspector] public string id;
    void Awake()
    {
        animator = GetComponent<Animator>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }

    public void Interact()
    {
        if (!triggered)
        {
            triggered = true;
            GetComponent<DialogueTrigger>().onFinishedDialogueCallback += StartBattle;
        }
    }

    private void StartBattle()
    {
        StartCoroutine(FindObjectOfType<EncounterManager>().LaunchCustomBattle(null, enemies, background));
    }
    private void Save(string temp)
    {
        SaveData.current.vineWeavers.Add(new VineWeaverGOData(gameObject.GetComponent<VineWeaverGO>()));
    }

    public void Load(string temp)
    {
        VineWeaverGOData data = SaveData.current.vineWeavers.Find(x => x.id == id);

        if (data != default)
        {
            triggered = data.triggered;

            if (triggered)
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
public class VineWeaverGOData
{
    public string id;
    public bool triggered;

    public VineWeaverGOData(VineWeaverGO vineWeaverGO)
    {
        id = vineWeaverGO.id;
        triggered = vineWeaverGO.triggered;
    }
}
