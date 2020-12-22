using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavefileDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI gaiaText;
    [SerializeField] TextMeshProUGUI gaText;
    [SerializeField] TextMeshProUGUI locationText;
    [SerializeField] TextMeshProUGUI playtimeText;
    [SerializeField] int savefileNr;
    [SerializeField] GameObject newGameHolder;
    private string sceneName;

    private void Awake()
    {
        Reload();
    }

    public void Reload()
    {
        SavefileDisplayData data = SaveSystem.Load<SavefileDisplayData>(savefileNr + "/" + "SavefileDisplay");

        if (data != default)
        {
            hpText.text = data.hp.ToString();
            gaiaText.text = data.gaia.ToString();
            gaText.text = data.ga.ToString();
            playtimeText.text = data.playtime;

            sceneName = data.location;
            string location = sceneName;

            switch (data.location)
            {
                case "OW_FOD":
                    location = "Field of Dreams";
                    break;
                case "OW_PW":
                    location = "Pitchblack Woods";
                    break;
                case "OW_RC":
                    location = "River Creek";
                    break;
                case "OW_SG":
                    location = "Sunken Garden";
                    break;
                case "OW_WE":
                    location = "Waters Edge";
                    break;
                case "OW_PP":
                    location = "Pumpkin Patch";
                    break;
            }

            locationText.text = location;
        }
        else
        {
            newGameHolder.SetActive(true);
            sceneName = null;
        }
    }

    public string GetSceneName()
    {
        return sceneName;
    }
}

[System.Serializable]
public class SavefileDisplayData
{
    public int hp;
    public int gaia;
    public int ga;
    public string location;
    public string playtime;

    public SavefileDisplayData(PlayerValues playerValues, TimeManager timeManager)
    {
        hp = playerValues.healthPoints;
        gaia = playerValues.gaia;
        ga = playerValues.currency;
        location = playerValues.currentOWScene;
        playtime = timeManager.GetTimeHoursSeconds();
    }
}