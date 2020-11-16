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
    public bool TriggerBerryChoice(BerryTile berryTile)
    {
        tileBeingPlanted = berryTile;
        InventoryUI inventoryUI;
        seedsToPlant.Clear();
        berryInfo.Clear();
        if ((inventoryUI = FindObjectOfType<InventoryUI>()) != null)
        {
            seedsToPlant = inventoryUI.GetActiveSeeds();
        }

        if(seedsToPlant.Count == 0)
        {
            return false;
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

        StartCoroutine(ShortWait(firstSlot));

        return true;
    }
     IEnumerator ShortWait(GameObject firstSlot)
    {
        yield return new WaitForEndOfFrame();
        playerControlsManager.ToggleOnGenericUI();
        eventSystem.SetSelectedGameObject(firstSlot);
    }

    public void ChooseBerry(BerryInfo bi)
    {
        int index = berryInfo.IndexOf(bi);
        tileBeingPlanted.PlantBerry(seedsToPlant[index]);
        eventSystem.SetSelectedGameObject(null);
        playerControlsManager.ToggleOffGenericUI();
        berryPanel.SetActive(false);
        foreach (BerryInfo berry in berryInfo)
        {
            Destroy(berry.gameObject);
        }
        seedsToPlant.Clear();
        berryInfo.Clear();
    }
}
