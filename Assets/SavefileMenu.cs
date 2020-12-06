using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SavefileMenu : MonoBehaviour
{
    [SerializeField] GameObject firstSlot;
    private LevelLoader levelLoader;
    [SerializeField] List<SavefileDisplay> savefileDisplays = new List<SavefileDisplay>();
    [SerializeField] PlayerValues playerValues;
    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Start()
    {
        eventSystem.SetSelectedGameObject(firstSlot);
    }

    public void ClickSave1()
    {
        playerValues.currentSavefile = 1;
        if ((savefileDisplays[0].GetSceneName() == null))
        {
            SaveSystem.ResetSavefile(1);
            levelLoader.LoadOverworldSceneFromMenu("OW_FOD");
        }
        else
        {
            levelLoader.LoadOverworldSceneFromMenu(savefileDisplays[0].GetSceneName());
        }
    }

    public void ClickSave2()
    {
        playerValues.currentSavefile = 2;
        if ((savefileDisplays[1].GetSceneName() == null))
        {
            SaveSystem.ResetSavefile(2);
            levelLoader.LoadOverworldSceneFromMenu("OW_FOD");
        }
        else
        {
            levelLoader.LoadOverworldSceneFromMenu(savefileDisplays[1].GetSceneName());
        }
    }

    public void ClickSave3()
    {
        playerValues.currentSavefile = 3;
        if ((savefileDisplays[2].GetSceneName() == null))
        {
            SaveSystem.ResetSavefile(3);
            levelLoader.LoadOverworldSceneFromMenu("OW_FOD");
        }
        else
        {
            levelLoader.LoadOverworldSceneFromMenu(savefileDisplays[2].GetSceneName());
        }
    }

    public void ClickDelete1()
    {
        SaveSystem.ResetSavefile(1);
    }
    public void ClickDelete2()
    {
        SaveSystem.ResetSavefile(2);
    }
    public void ClickDelete3()
    {
        SaveSystem.ResetSavefile(3);
    }

    public void ClickBack()
    {

    }
}
