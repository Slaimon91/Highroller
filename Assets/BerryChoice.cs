using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BerryChoice : MonoBehaviour
{
    [SerializeField] GameObject berryHolder;
    [SerializeField] BerryInfo firstBerryInfoHolder;
    [SerializeField] BerryInfo additionalBerryInfoHolder;
    [SerializeField] GameObject berryPanel;
    private BerryTile tileBeingPlanted;
    private List<SeedBase> seedsToPlant = new List<SeedBase>();
    private List<BerryInfo> berryInfo = new List<BerryInfo>();
    private bool firstSeed = true;
    private PlayerControlsManager playerControlsManager;
    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        playerControlsManager = FindObjectOfType<PlayerControlsManager>();
    }
    public void TriggerBerryChoice(BerryTile berryTile)
    {
        tileBeingPlanted = berryTile;
        InventoryUI inventoryUI;
        if ((inventoryUI = FindObjectOfType<InventoryUI>()) != null)
        {
            seedsToPlant = inventoryUI.GetSeeds();
        }

        firstSeed = true;
        berryPanel.SetActive(true);
        GameObject firstSlot = null;

        foreach (SeedBase seed in seedsToPlant)
        {
            BerryInfo berry;

            if(firstSeed)
            {
                firstSeed = false;
                berry = Instantiate(firstBerryInfoHolder, berryHolder.transform);
                firstSlot = berry.gameObject;
            }
            else
            {
                berry = Instantiate(additionalBerryInfoHolder, berryHolder.transform);
            }

            berry.BerryInfoSetup(seed.GetSeedSprite(), seed.GetSeedName(), gameObject.GetComponent<BerryChoice>());
            berryInfo.Add(berry);
        }

        playerControlsManager.ToggleOnGenericUI();
        eventSystem.SetSelectedGameObject(firstSlot);
    }

    public void ChooseBerry(BerryInfo bi)
    {
        int index = berryInfo.IndexOf(bi);
        tileBeingPlanted.PlantBerry(seedsToPlant[index]);
        playerControlsManager.ToggleOffGenericUI();
        //berryPanel.SetActive(false);
    }
}
