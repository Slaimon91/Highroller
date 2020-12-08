using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savestone : MonoBehaviour, IInteractable
{
    private Animator animator;
    [HideInInspector]
    public bool isActivated = false;
    private bool activateAnimDone = false;

    void Awake()
    {
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
        SaveSystem.Save<SavestoneData>(new SavestoneData(gameObject.GetComponent<Savestone>()), "", FindObjectOfType<PlayerController>().playerValues.currentSavefile + "/" + temp + FindObjectOfType<PlayerController>().playerValues.currentOWScene + "/SaveStones");
    }

    public void Load(string temp)
    {
        SavestoneData data = SaveSystem.Load<SavestoneData>("", FindObjectOfType<PlayerController>().playerValues.currentSavefile + "/" + temp + FindObjectOfType<PlayerController>().playerValues.currentOWScene + "/SaveStones");

        if(data != default)
        {
            isActivated = data.isActivated;
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
    public bool isActivated;
    public Vector3 position;

    public SavestoneData(Savestone savestone)
    {
        isActivated = savestone.isActivated;
        position = savestone.gameObject.transform.position;
    }
}

