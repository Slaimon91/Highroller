using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savestone : MonoBehaviour, IInteractable
{
    private Animator animator;
    //[HideInInspector]
    public bool isActivated = false;
    [HideInInspector] public string id;
    //[HideInInspector]
    public bool activateAnimDone = false;

    void Awake()
    {
        id = GetComponent<UniqueID>().id;
        animator = GetComponent<Animator>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    public void Interact()
    {
        if (isActivated && activateAnimDone)
        {
            animator.SetBool("isSaving", true);
            FindObjectOfType<PlayerControlsManager>().ToggleOnGenericUI();
            GameEvents.OnSaveInitiated();
        }
        else
        {
            isActivated = true;
            animator.SetTrigger("isActivated");
        }
    }

    public void FinishedSaveAnim()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
        animator.SetBool("isSaving", false);
    }

    public void FinishedActivateAnim()
    {
        activateAnimDone = true;
    }

    private void Save(string temp)
    {
        //SaveSystem.Save<SavestoneData>(new SavestoneData(gameObject.GetComponent<Savestone>()), "", FindObjectOfType<PlayerController>().playerValues.currentSavefile + "/" + temp + FindObjectOfType<PlayerController>().playerValues.currentOWScene + "/SaveStones");
        SaveData.current.saveStones.Add(new SavestoneData(gameObject.GetComponent<Savestone>()));
        Debug.Log("Saved stone with id:" + id + " and status: " + isActivated);
    }

    public void Load(string temp)
    {
        //SavestoneData data = SaveSystem.Load<SavestoneData>("", FindObjectOfType<PlayerController>().playerValues.currentSavefile + "/" + temp + FindObjectOfType<PlayerController>().playerValues.currentOWScene + "/SaveStones");
        SavestoneData data = SaveData.current.saveStones.Find(x => x.id == id);
        Debug.Log("Searching stone has id:" + id);

        if (data != default)
        {
            Debug.Log("Loading stone, " + isActivated + " = " + data.isActivated);
            isActivated = data.isActivated;
            activateAnimDone = data.activateAnimDone;
            transform.position = data.position;

            if (isActivated)
            {
                animator.Play("Savestone idle 2");
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
public class SavestoneData
{
    public string id;
    public bool isActivated;
    public bool activateAnimDone;
    public Vector3 position;

    public SavestoneData(Savestone savestone)
    {
        id = savestone.id;
        isActivated = savestone.isActivated;
        activateAnimDone = savestone.isActivated;
        position = savestone.gameObject.transform.position;
    }
}

