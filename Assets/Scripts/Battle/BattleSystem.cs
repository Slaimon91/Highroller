﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    //Enemies and abilities
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    private List<GameObject> enemiesGO = new List<GameObject>();
    private List<EnemyInfo> enemiesInfo = new List<EnemyInfo>();
    private List<BattleAbilityHolder> abilityHolder = new List<BattleAbilityHolder>();
    [HideInInspector] public List<AbilityBase> battleAbilites = new List<AbilityBase>();
    private GameObject[] enemySpotOccupied = {null, null, null, null};

    //Spawnpoints
    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;
    public Transform rightOOB;
    public Transform leftOOB;

    //Dice and dice keys
    private int[] diceNumbers;
    private List<Image> diceImages = new List<Image>();
    public Sprite[] diceSprites;
    public Sprite[] diceSpritesGold;
    public Sprite[] diceSpritesPlatinum;
    public Sprite[] diceSpritesInactive;
    private List<Dice> diceObjects = new List<Dice>();
    private int[] firstDicePair = { -1, -1 };
    private int[] secondDicePair = { -1, -1 };
    private int[] thirdDicePair = { -1, -1 };

    private List<int> diceKeyNumbers = new List<int>();
    private List<Image> diceKeyImages = new List<Image>();
    private List<DiceKey> diceKeys = new List<DiceKey>();

    //Prefabs and panels
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Dice dicePrefabs;
    [SerializeField] DiceKey diceKeyPrefab;
    [SerializeField] EnemyInfo enemiesInfoPrefab;
    [SerializeField] BattleAbilityHolder abilitiesPrefab;
    [SerializeField] BattleAbilityHolder abilitiesPrefabThree;
    [SerializeField] Transform dicePanel;
    [SerializeField] Transform diceKeyPanel;
    [SerializeField] Transform enemiesInfoPanel;
    [SerializeField] Transform abilitiesPanel;
    [SerializeField] Transform abilitiesPanelThree;
    [SerializeField] Image buttonInfoBox;
    [SerializeField] Sprite threeAbilitiesSprite;
    [SerializeField] Sprite playerturnButtonBox;
    [SerializeField] Sprite enemyturnButtonBox;
    [SerializeField] Sprite whiteAbilityBorder;
    [SerializeField] BattleStartupInfo battleStartupInfo;

    public BattleState state;
    private GameObject lastselect;
    private PlayerBattleController player;
    private bool cancelPressed = false;
    private int enemiesToDie = 0;
    private Sprite battleBackground;
    private EventSystem eventSystem;
    private AudioManager audioManager;
    private ButtonPanel buttonPanel;
    private BattleBounty battleBounty;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        state = BattleState.START;
        audioManager = FindObjectOfType<AudioManager>();
        eventSystem = FindObjectOfType<EventSystem>();
        buttonPanel = FindObjectOfType<ButtonPanel>();
        battleBounty = FindObjectOfType<BattleBounty>();
        SetUpBattle();
    }

    void Update()
    {
        //PreventCursor();
        //PressCancel();
    }

    void SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawnPoint);
        InstantiateDices();
        SpawnMonstersStartup();
        GetComponentInChildren<SpriteRenderer>().sprite = battleStartupInfo.battleBackground;
        player = playerGO.GetComponent<PlayerBattleController>();
        SetupButtonNavigation();
        state = BattleState.PLAYERTURN;
        FirstTurn();
    }

    public void SetDiceKey(int index)
    {
        EnemyBattleBase enemy = enemiesGO[index].GetComponent<EnemyBattleBase>();
        diceKeyNumbers[index] = enemy.GetDiceKeyNumber();


        if (enemy.GetInactiveStatus())
        {
            diceKeys[index].SetInactive(true, enemy.GetDiceKeyNumber());
            diceKeyImages[index].sprite = diceSpritesInactive[diceKeyNumbers[index] - 1];
        }
        else if (enemy.GetGoldStatus())
        {
            diceKeys[index].SetInactive(false, enemy.GetDiceKeyNumber());
            diceKeys[index].SetGold(true, enemy.GetDiceKeyNumber());
            diceKeyImages[index].sprite = diceSpritesGold[diceKeyNumbers[index] - 1];
        }
        else if (enemy.GetPlatinumStatus())
        {
            diceKeys[index].SetInactive(false, enemy.GetDiceKeyNumber());
            diceKeys[index].SetPlatinum(true, enemy.GetDiceKeyNumber());
            diceKeyImages[index].sprite = diceSpritesPlatinum[diceKeyNumbers[index] - 1];
        }
        else
        {
            diceKeys[index].SetInactive(false, enemy.GetDiceKeyNumber());
            diceKeyImages[index].sprite = diceSprites[diceKeyNumbers[index] - 1];
        }
    }

    public void ActivateNextDK(DiceKey DK, string status, int number)
    {
        int index = 0;
        
        for (int i = 0; i < diceKeys.Count; i++)
        {
            if(diceKeys[i] == DK)
            {
                index = i;
            }
        }

        if (status == "deactivated")
        {
            diceKeys[index].SetInactive(true, number);
            diceKeys[index].SetGold(false, number);
            diceKeys[index].SetPlatinum(false, number);
            diceKeyImages[index].sprite = diceSpritesInactive[number - 1];
        }
        else if (status == "gold")
        {
            diceKeys[index].SetInactive(false, number);
            diceKeys[index].SetGold(true, number);
            diceKeys[index].SetPlatinum(false, number);
            diceKeyImages[index].sprite = diceSpritesGold[number - 1];
        }
        else if (status == "plat")
        {
            diceKeys[index].SetInactive(false, number);
            diceKeys[index].SetGold(false, number);
            diceKeys[index].SetPlatinum(true, number);
            diceKeyImages[index].sprite = diceSpritesPlatinum[number - 1];
        }
        else
        {
            diceKeys[index].SetInactive(false, number);
            diceKeys[index].SetGold(false, number);
            diceKeys[index].SetPlatinum(false, number);
            diceKeyImages[index].sprite = diceSprites[number - 1];
        }

        DK.SetAssignedStatus(false, number);
        diceKeyNumbers[index] = number;
    }

    void InstantiateDices()
    {
        //Instantiate dices
        if (battleStartupInfo.nrOfDices <= 3)
        {
            int offset = 46;
            for(int i = 0; i < battleStartupInfo.nrOfDices; i++)
            {
                diceObjects.Add(Instantiate(dicePrefabs, dicePanel.transform));
                RectTransform rt = diceObjects[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(46 - offset*i, 0, 0);
                diceImages.Add(diceObjects[i].GetComponent<Image>());
                diceObjects[i].GetComponent<Button>().onClick.AddListener(OnDicePressed);
            }

            diceNumbers = new int[] { 0, 0, 0 };
        }
        else if (battleStartupInfo.nrOfDices == 4)
        {
            int offset = 37;
            int extraoffset = 0;
            for (int i = 0; i < battleStartupInfo.nrOfDices; i++)
            {
                if(i == 2)
                {
                    extraoffset = -1;
                }

                diceObjects.Add(Instantiate(dicePrefabs, dicePanel.transform));
                RectTransform rt = diceObjects[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(56 - (offset * i) + extraoffset, 0, 0);
                diceImages.Add(diceObjects[i].GetComponent<Image>());
                diceObjects[i].GetComponent<Button>().onClick.AddListener(OnDicePressed);
            }

            diceNumbers = new int[] { 0, 0, 0, 0 };
        }
        else if (battleStartupInfo.nrOfDices >= 5)
        {
            int offset = 31;
            for (int i = 0; i < battleStartupInfo.nrOfDices; i++)
            {
                diceObjects.Add(Instantiate(dicePrefabs, dicePanel.transform));
                RectTransform rt = diceObjects[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(62 - offset * i, 0, 0);
                diceImages.Add(diceObjects[i].GetComponent<Image>());
                diceObjects[i].GetComponent<Button>().onClick.AddListener(OnDicePressed);
            }

            diceNumbers = new int[] { 0, 0, 0, 0, 0 };
        }

        eventSystem.firstSelectedGameObject = diceObjects[0].gameObject;

        //Instantiate player abilities
        if(FindObjectOfType<InventoryUI>() != null)
        {
            List<AbilityBase> equippedAbilities = FindObjectOfType<InventoryUI>().GetPlayerAbilities();

            for (int i = 0; i < equippedAbilities.Count; i++)
            {
                if (equippedAbilities.Count == 1 || equippedAbilities.Count == 2)
                {
                    int offset = 22;

                    abilityHolder.Add(Instantiate(abilitiesPrefab, abilitiesPanel.transform));
                    RectTransform rt = abilityHolder[i].GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector3(0 + offset * i, 0, 0);
                    Image abilitiesImage = abilityHolder[i].GetComponent<Image>();
                    abilityHolder[i].SetAbilityName(equippedAbilities[i].GetComponent<AbilityBase>().GetAbilityName());
                    abilityHolder[i].SetAbilityText(equippedAbilities[i].GetComponent<AbilityBase>().GetInfo());

                    battleAbilites.Add(Instantiate(equippedAbilities[i], abilityHolder[i].transform));
                }

                else if (equippedAbilities.Count == 3)
                {
                    abilitiesPanel.gameObject.SetActive(false);
                    abilitiesPanelThree.gameObject.SetActive(true);

                    int offset = 22;

                    abilityHolder.Add(Instantiate(abilitiesPrefabThree, abilitiesPanelThree.transform));
                    RectTransform rt = abilityHolder[i].GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector3(0 + offset * i, 0, 0);
                    Image abilitiesImage = abilityHolder[i].GetComponent<Image>();
                    //abilitiesImage.sprite = battleStartupInfo.abilities[i].GetComponent<SpriteRenderer>().sprite;
                    abilityHolder[i].SetAbilityName(equippedAbilities[i].GetComponent<AbilityBase>().GetAbilityName());
                    abilityHolder[i].SetAbilityText(equippedAbilities[i].GetComponent<AbilityBase>().GetInfo());

                    battleAbilites.Add(Instantiate(equippedAbilities[i], abilityHolder[i].transform));
                }
            }
        }
    }

    public void SpawnMonstersStartup()
    {
        //Setup
        int k = 0;

        //Instantiate enemies and dicekeys
        for (int i = 0; i < battleStartupInfo.enemies.Count; i++)
        {
            if (battleStartupInfo.enemies[i] == null)
            {
                k++;
                continue;
            }

            int offset = 31;
            int offsetInfo = 22;
            diceKeys.Add(Instantiate(diceKeyPrefab, diceKeyPanel.transform));
            RectTransform rt = diceKeys[i - k].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0 - offset * i, 0, 0);
            diceKeyImages.Add(diceKeys[i - k].GetComponent<Image>());
            diceKeys[i - k].GetComponent<Button>().onClick.AddListener(OnKeyPressed);

            enemiesInfo.Add(Instantiate(enemiesInfoPrefab, enemiesInfoPanel.transform));
            RectTransform rtInfo = enemiesInfo[i - k].GetComponent<RectTransform>();
            rtInfo.anchoredPosition = new Vector3(0 - offsetInfo * i, 0, 0);
            Image enemiesInfoImage = enemiesInfo[i - k].GetEnemyPortrait();
            enemiesInfoImage.sprite = battleStartupInfo.enemies[i].GetComponent<EnemyBattleBase>().GetIcon();
            enemiesInfo[i - k].SetUnitName(battleStartupInfo.enemies[i].GetComponent<EnemyBattleBase>().GetUnitName());
            enemiesInfo[i - k].SetUnitText(battleStartupInfo.enemies[i].GetComponent<EnemyBattleBase>().GetInfoText());

            enemyPrefabs.Add(battleStartupInfo.enemies[i]);
            var enemyGO = Instantiate(enemyPrefabs[i - k], enemySpawnPoints[i]);
            enemyGO.GetComponentInChildren<SpriteRenderer>().sortingOrder = battleStartupInfo.enemies.Count - k;
            enemiesGO.Add(enemyGO);
            diceKeyNumbers.Add(enemyGO.GetComponent<EnemyBattleBase>().GetDiceKeyNumber());
            SetDiceKey(i - k);
            enemyGO.GetComponent<EnemyBattleBase>().SetDiceKeyGO(diceKeys[i - k]);
            enemySpotOccupied[k] = enemyGO;
        }
    }

    public IEnumerator SpawnNewMonster(GameObject enemyPrefab)
    {
        int spawnOffset = 0;
        int offset = 31;
        int offsetInfo = 22;

        if(enemyPrefab.GetComponent<EnemyBattleBase>().GetPreferedSpawnLocation() != -1)
        {
            if(enemySpotOccupied[enemyPrefab.GetComponent<EnemyBattleBase>().GetPreferedSpawnLocation()] != true)
            {
                spawnOffset = enemyPrefab.GetComponent<EnemyBattleBase>().GetPreferedSpawnLocation();
            }
        }

        if(spawnOffset == 0)
        {
            for(int j = 0; j < enemySpotOccupied.Length; j++)
            {
                if(enemySpotOccupied[j] == null)
                {
                    spawnOffset = j;
                    break;
                }
            }
        }

        int i = enemiesGO.Count;
        diceKeys.Add(Instantiate(diceKeyPrefab, diceKeyPanel.transform));
        RectTransform rt = diceKeys[i].GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(0 - offset * spawnOffset, 0, 0);
        diceKeyImages.Add(diceKeys[i].GetComponent<Image>());
        diceKeys[i].GetComponent<Button>().onClick.AddListener(OnKeyPressed);

        enemiesInfo.Add(Instantiate(enemiesInfoPrefab, enemiesInfoPanel.transform));
        RectTransform rtInfo = enemiesInfo[i].GetComponent<RectTransform>();
        rtInfo.anchoredPosition = new Vector3(0 - offsetInfo * spawnOffset, 0, 0);
        Image enemiesInfoImage = enemiesInfo[i].GetEnemyPortrait();
        enemiesInfoImage.sprite = enemyPrefab.GetComponent<EnemyBattleBase>().GetIcon();
        enemiesInfo[i].SetUnitName(enemyPrefab.GetComponent<EnemyBattleBase>().GetUnitName());
        enemiesInfo[i].SetUnitText(enemyPrefab.GetComponent<EnemyBattleBase>().GetInfoText());

        enemyPrefabs.Add(enemyPrefab);
        var enemyGO = Instantiate(enemyPrefabs[i], enemySpawnPoints[spawnOffset]);
        enemyGO.GetComponentInChildren<SpriteRenderer>().sortingOrder = i - spawnOffset;
        enemiesGO.Add(enemyGO);
        diceKeyNumbers.Add(enemyGO.GetComponent<EnemyBattleBase>().GetDiceKeyNumber());
        SetDiceKey(i);
        enemyGO.GetComponent<EnemyBattleBase>().SetDiceKeyGO(diceKeys[i]);
        enemySpotOccupied[spawnOffset] = enemyGO;

        yield return null;
    }

    void CheckSpawnLocationAvailability()
    {

    }

    void SetupButtonNavigation()
    {
        //eventSystem.SetSelectedGameObject(diceObjects[0].gameObject);
        //Dices
        for (int i = 0; i < diceObjects.Count; i++)
        {
            if (i + 1 < diceObjects.Count)
            {
                diceObjects[i].SetButtonNavigation(diceObjects[i+1].GetComponent<Button>(), "left");
            }

            if(i != 0)
            {
                diceObjects[i].SetButtonNavigation(diceObjects[i - 1].GetComponent<Button>(), "right");
            }

            int shortestDistanceKey = 0;
            float shortestDistanceKeyUnits = 0;

            for(int k = 0; k < diceKeys.Count; k++)
            {
                float distance = Vector2.Distance(diceObjects[i].GetComponent<RectTransform>().TransformPoint(diceObjects[i].GetComponent<RectTransform>().rect.center),
                                        diceKeys[k].GetComponent<RectTransform>().TransformPoint(diceKeys[k].GetComponent<RectTransform>().rect.center));

                if (distance < shortestDistanceKeyUnits || k == 0)
                {
                    shortestDistanceKey = k;
                    shortestDistanceKeyUnits = distance;
                }
            }

            diceObjects[i].SetButtonNavigation(diceKeys[shortestDistanceKey].GetComponent<Button>(), "up");
        }

        //Dice keys
        for(int i = 0; i < diceKeys.Count; i++)
        {
            if (i + 1 < diceKeys.Count)
            {
                diceKeys[i].SetButtonNavigation(diceKeys[i + 1].GetComponent<Button>(), "left");
            }

            if (i != 0)
            {
                diceKeys[i].SetButtonNavigation(diceKeys[i - 1].GetComponent<Button>(), "right");
            }

            int shortestDistanceDice = 0;
            float shortestDistanceDiceUnits = 0;

            for (int k = 0; k < diceObjects.Count; k++)
            {
                float distance = Vector2.Distance(diceKeys[i].GetComponent<RectTransform>().TransformPoint(diceKeys[i].GetComponent<RectTransform>().rect.center),
                                                        diceObjects[k].GetComponent<RectTransform>().TransformPoint(diceObjects[k].GetComponent<RectTransform>().rect.center));

                if (distance < shortestDistanceDiceUnits || k == 0)
                {
                    shortestDistanceDice = k;
                    shortestDistanceDiceUnits = distance;
                }
            }

            diceKeys[i].SetButtonNavigation(diceObjects[shortestDistanceDice].GetComponent<Button>(), "down");

            int shortestDistanceInfo = 0;
            float shortestDistanceInfoUnits = 0;

            for (int k = 0; k < enemiesInfo.Count; k++)
            {
                float distance = Vector2.Distance(diceKeys[i].GetComponent<RectTransform>().TransformPoint(diceKeys[i].GetComponent<RectTransform>().rect.center),
                                                        enemiesInfo[k].GetComponent<RectTransform>().TransformPoint(enemiesInfo[k].GetComponent<RectTransform>().rect.center));

                if (distance < shortestDistanceInfoUnits || k == 0)
                {
                    shortestDistanceInfo = k;
                    shortestDistanceInfoUnits = distance;
                }


            }

            diceKeys[i].SetButtonNavigation(enemiesInfo[shortestDistanceInfo].GetComponent<Button>(), "up");
        }

        //Enemies Info
        for (int i = 0; i < enemiesInfo.Count; i++)
        {
            if (i + 1 < enemiesInfo.Count)
            {
                enemiesInfo[i].SetButtonNavigation(enemiesInfo[i + 1].GetComponent<Button>(), "left");
            }

            if (i != 0)
            {
                enemiesInfo[i].SetButtonNavigation(enemiesInfo[i - 1].GetComponent<Button>(), "right");
            }

            if(i == enemiesInfo.Count - 1)
            {
                if(abilityHolder.Count > 0)
                {
                    enemiesInfo[enemiesInfo.Count - 1].SetButtonNavigation(abilityHolder[abilityHolder.Count - 1].GetComponent<Button>(), "left");
                }
            }

            int shortestDistanceKey = 0;
            float shortestDistanceKeyUnits = 0;

            for (int k = 0; k < diceKeys.Count; k++)
            {
                float distance = Vector2.Distance(enemiesInfo[i].GetComponent<RectTransform>().TransformPoint(enemiesInfo[i].GetComponent<RectTransform>().rect.center),
                                        diceKeys[k].GetComponent<RectTransform>().TransformPoint(diceKeys[k].GetComponent<RectTransform>().rect.center));

                if (distance < shortestDistanceKeyUnits || k == 0)
                {
                    shortestDistanceKey = k;
                    shortestDistanceKeyUnits = distance;
                }
            }

            enemiesInfo[i].SetButtonNavigation(diceKeys[shortestDistanceKey].GetComponent<Button>(), "down");
        }

        //Abilities Info
        for (int i = 0; i < abilityHolder.Count; i++)
        {
            if (i + 1 < abilityHolder.Count)
            {
                abilityHolder[i].SetButtonNavigation(abilityHolder[i + 1].GetComponent<Button>(), "right");
            }

            if (i != 0)
            {
                abilityHolder[i].SetButtonNavigation(abilityHolder[i - 1].GetComponent<Button>(), "left");
            }

            if (i == abilityHolder.Count - 1)
            {
                if (enemiesInfo.Count > 0)
                {
                    abilityHolder[abilityHolder.Count - 1].SetButtonNavigation(enemiesInfo[enemiesInfo.Count - 1].GetComponent<Button>(), "right");
                }
            }

            abilityHolder[i].SetButtonNavigation(diceKeys[diceKeys.Count - 1].GetComponent<Button>(), "down");
        }

    }

    void FirstTurn()
    {
        SetupButtonNavigation();
        state = BattleState.PLAYERTURN;
        RollDice();
        buttonInfoBox.sprite = playerturnButtonBox;
    }

    void PlayerTurn()
    {
        audioManager.Play("PlayerTurnStart");
        SetupButtonNavigation();
        state = BattleState.PLAYERTURN;
        buttonInfoBox.sprite = playerturnButtonBox;
        FindObjectOfType<HoldAssignButton>().Reset();
        RollDice();
        foreach (AbilityBase ability in battleAbilites)
        {
            ability.TurnStart();
        }
    }

    public void RollDice()
    {
        for (int i = 0; i < diceObjects.Count; i++)
        {
            if (diceObjects[i].GetAssignedStatus())
            {
                diceObjects[i].SetAssignedStatus(false);
                diceObjects[i].SetAssignedTo(null);
            }

            if (!diceObjects[i].GetLockedOrInactiveStatus() && !diceObjects[i].GetComponent<Dice>().GetGoldStatus() && !diceObjects[i].GetComponent<Dice>().GetPlatinumStatus())
            {
                diceNumbers[i] = Random.Range(1, 7);
                diceImages[i].sprite = diceSprites[diceNumbers[i] - 1];
            }
            else if (!diceObjects[i].GetLockedOrInactiveStatus() && diceObjects[i].GetComponent<Dice>().GetGoldStatus())
            {
                Dice pressedDice = diceObjects[i].GetComponent<Dice>();

                if (firstDicePair[1] != -1)
                {
                    if (pressedDice == diceObjects[firstDicePair[1]])
                    {
                        diceObjects[firstDicePair[1]].SetGold(false, -1);
                        diceObjects[firstDicePair[0]].SetInactiveStatus(false);
                        diceNumbers[firstDicePair[1]] -= diceNumbers[firstDicePair[0]];
                        diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                        diceNumbers[firstDicePair[0]] = Random.Range(1, 7);
                        diceNumbers[firstDicePair[1]] = Random.Range(1, 7);
                        diceImages[firstDicePair[0]].sprite = diceSprites[diceNumbers[firstDicePair[0]] - 1];
                        diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                        firstDicePair[0] = -1;
                        firstDicePair[1] = -1;
                    }
                }

                if (secondDicePair[1] != -1)
                {
                    if (pressedDice == diceObjects[secondDicePair[1]])
                    {
                        diceObjects[secondDicePair[1]].SetGold(false, -1);
                        diceObjects[secondDicePair[0]].SetInactiveStatus(false);
                        diceNumbers[secondDicePair[1]] -= diceNumbers[secondDicePair[0]];
                        diceImages[secondDicePair[1]].sprite = diceSprites[diceNumbers[secondDicePair[1]] - 1];
                        diceNumbers[secondDicePair[0]] = Random.Range(1, 7);
                        diceNumbers[secondDicePair[1]] = Random.Range(1, 7);
                        diceImages[secondDicePair[0]].sprite = diceSprites[diceNumbers[secondDicePair[0]] - 1];
                        diceImages[secondDicePair[1]].sprite = diceSprites[diceNumbers[secondDicePair[1]] - 1];
                        secondDicePair[0] = -1;
                        secondDicePair[1] = -1;
                    }
                }
            }
            else if (!diceObjects[i].GetLockedOrInactiveStatus() && diceObjects[i].GetComponent<Dice>().GetPlatinumStatus())
            {
                Dice pressedDice = diceObjects[i].GetComponent<Dice>();

                if (thirdDicePair[1] != -1)
                {
                    if (pressedDice == diceObjects[thirdDicePair[1]])
                    {
                        //Split platinum
                        diceObjects[thirdDicePair[1]].SetPlatinum(false, -1);
                        diceObjects[thirdDicePair[0]].SetInactiveStatus(false);
                        diceNumbers[thirdDicePair[1]] -= diceNumbers[thirdDicePair[0]];
                        diceObjects[thirdDicePair[0]].SetGold(true, diceNumbers[thirdDicePair[0]]);
                        diceObjects[thirdDicePair[1]].SetGold(true, diceNumbers[thirdDicePair[1]]);
                        diceImages[thirdDicePair[1]].sprite = diceSpritesGold[diceNumbers[thirdDicePair[1]] - 1];

                        thirdDicePair[0] = -1;
                        thirdDicePair[1] = -1;

                        //Split first gold
                        diceObjects[firstDicePair[1]].SetGold(false, -1);
                        diceObjects[firstDicePair[0]].SetInactiveStatus(false);
                        diceNumbers[firstDicePair[1]] -= diceNumbers[firstDicePair[0]];
                        diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                        diceNumbers[firstDicePair[0]] = Random.Range(1, 7);
                        diceNumbers[firstDicePair[1]] = Random.Range(1, 7);
                        diceImages[firstDicePair[0]].sprite = diceSprites[diceNumbers[firstDicePair[0]] - 1];
                        diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                        firstDicePair[0] = -1;
                        firstDicePair[1] = -1;

                        //Split second gold
                        diceObjects[secondDicePair[1]].SetGold(false, -1);
                        diceObjects[secondDicePair[0]].SetInactiveStatus(false);
                        diceNumbers[secondDicePair[1]] -= diceNumbers[secondDicePair[0]];
                        diceImages[secondDicePair[1]].sprite = diceSprites[diceNumbers[secondDicePair[1]] - 1];
                        diceNumbers[secondDicePair[0]] = Random.Range(1, 7);
                        diceNumbers[secondDicePair[1]] = Random.Range(1, 7);
                        diceImages[secondDicePair[0]].sprite = diceSprites[diceNumbers[secondDicePair[0]] - 1];
                        diceImages[secondDicePair[1]].sprite = diceSprites[diceNumbers[secondDicePair[1]] - 1];
                        secondDicePair[0] = -1;
                        secondDicePair[1] = -1;
                    }
                }
            }

            if (diceObjects[i].GetLockedStatus())
            {
                diceObjects[i].UnlockDice();
            }
            diceObjects[i].SetMarkedStatus(false);
        }
    }

    IEnumerator EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        buttonInfoBox.sprite = enemyturnButtonBox;
        List<GameObject> toRemoveList = new List<GameObject>();

        //see if someone will die
        foreach(var enemyGO in enemiesGO)
        {
            if (enemyGO.GetComponent<EnemyBattleBase>().GetAssignedStatus())
            {
                toRemoveList.Add(enemyGO);
            }
        }

        foreach (AbilityBase ability in battleAbilites)
        {
            ability.TurnEnd();
        }

        yield return StartCoroutine(EnemyDeaths(toRemoveList));

        for (int i = toRemoveList.Count - 1; i >= 0; i--)
        {
            foreach (AbilityBase ability in battleAbilites)
            {
                ability.EnemyDeath();
            }
        }

        //See if someones front DK was assigned
        List<GameObject> tempGO = new List<GameObject>();
        foreach (var enemyGO in enemiesGO)
        {
            tempGO.Add(enemyGO);
        }
        foreach (var enemyGO in tempGO)
        {
            if (enemyGO.GetComponent<EnemyBattleBase>().GetFrontDKAssignedStatus())
            {
                yield return StartCoroutine(enemyGO.GetComponent<EnemyBattleBase>().ActivateNextDK());
            }
        }

        if (enemiesGO.Count == 0) //Battle is over
        {
            yield return StartCoroutine(WaitSec(2f));
            FindObjectOfType<HoldAssignButton>().Reset();
            FindObjectOfType<PlayerControlsManager>().ToggleOnGenericUI();
            battleBounty.GiveBounty();
        }
        else
        {
            //Launch enemy attacks
            yield return StartCoroutine(EnemyAttacks());
            yield return StartCoroutine(EnemySetups());
            PlayerTurn();
        }
    }

    IEnumerator EnemyDeaths(List<GameObject> sentToRemoveList)
    {
        enemiesToDie = sentToRemoveList.Count;

        int xpToAdd = 0;
        int xpToMultiply = enemiesToDie;

        //Actually kill them
        for (int i = sentToRemoveList.Count - 1; i >= 0; i--)
        {
            int index = enemiesGO.IndexOf(sentToRemoveList[i]);
            for (int j = 0; j < enemySpotOccupied.Length; j++)
            {
                if(enemySpotOccupied[j] != null)
                {
                    if (enemySpotOccupied[j].gameObject == enemiesGO[index].gameObject)
                    {
                        enemySpotOccupied[j] = null;
                    }
                }
            }
            xpToAdd += enemiesGO[index].GetComponent<EnemyBattleBase>().GetXPAmount();
            enemiesGO[index].GetComponent<EnemyBattleBase>().TriggerDying();
            GameObject emGO = enemiesInfo[index].gameObject;
            GameObject dkGO = diceKeys[index].gameObject;
            enemiesGO.Remove(enemiesGO[index]);
            enemiesInfo.Remove(enemiesInfo[index]);
            diceKeys.Remove(diceKeys[index]);
            diceKeyImages.Remove(diceKeyImages[index]);
            diceKeyNumbers.Remove(diceKeyNumbers[index]);
            Destroy(emGO);
            Destroy(dkGO);
            
        }
        
        while(enemiesToDie >= 1)
        {
            yield return null;
        }
        if(sentToRemoveList.Count != 0)
        {
            //At least one enemy died
        }

        if(xpToAdd > 0)
        {
            battleBounty.AddXP(xpToAdd);
            battleBounty.SetXPMultiplier(xpToMultiply);
        }
        
        yield return null;

    }

    public void SignalEnemyDeath()
    {
        enemiesToDie--;
    }

    IEnumerator EnemyAttacks()
    {
        player.ResetAction();
        //Enemy Attack
        for (int i = enemiesGO.Count - 1; i >= 0; i--)
        {

            yield return StartCoroutine(enemiesGO[i].GetComponent<EnemyBattleBase>().EnemyAction());
        }
    }

    IEnumerator EnemySetups()
    {
        //Enemy Setup
        for (int i = enemiesGO.Count - 1; i >= 0; i--)
        {
            yield return StartCoroutine(enemiesGO[i].GetComponent<EnemyBattleBase>().EnemySetup());
        }
    }

    public void OnDicePressed()
    {
        Dice secondPressedDice;
        var go = EventSystem.current.currentSelectedGameObject;
        if(go != null)
        {
            secondPressedDice = go.GetComponent<Dice>();
            if(!secondPressedDice.GetLockedOrInactiveStatus())
            {
                Dice firstPressedDice = null;
                int firstPressedNumber = 0;
                int secondPressedNumber = 0;

                for (int i = 0; i < diceObjects.Count; i++)
                {
                    if (diceObjects[i].GetMarkedStatus())
                    {
                        if (diceObjects[i] != secondPressedDice)
                        {
                            firstPressedDice = diceObjects[i];
                            firstPressedNumber = i;
                        }
                        else
                        {
                            secondPressedNumber = i;
                        }
                    }
                }
                
                if(firstPressedDice != null)
                {
                    //If none of them is already a pair
                    if (!diceObjects[secondPressedNumber].GetComponent<Dice>().GetGoldStatus() && 
                        !diceObjects[firstPressedNumber].GetComponent<Dice>().GetGoldStatus() && 
                        !diceObjects[secondPressedNumber].GetComponent<Dice>().GetPlatinumStatus() &&
                        !diceObjects[firstPressedNumber].GetComponent<Dice>().GetPlatinumStatus())
                    {
                        //Save die pair for later
                        if(firstDicePair[0] == -1)
                        {
                            firstDicePair[0] = firstPressedNumber;
                            firstDicePair[1] = secondPressedNumber;
                        }
                        else
                        {
                            secondDicePair[0] = firstPressedNumber;
                            secondDicePair[1] = secondPressedNumber;
                        }

                        //Add die operations
                        diceNumbers[secondPressedNumber] += diceNumbers[firstPressedNumber];
                        diceObjects[firstPressedNumber].SetInactiveStatus(true);
                        diceObjects[firstPressedNumber].SetMarkedStatus(false);
                        diceObjects[secondPressedNumber].SetMarkedStatus(false);
                        diceImages[secondPressedNumber].sprite = diceSpritesGold[diceNumbers[secondPressedNumber] - 1];
                        diceObjects[secondPressedNumber].SetGold(true, diceNumbers[secondPressedNumber]);
                        //diceObjects[secondPressedNumber].SetMarkedStatus(true);
                        audioManager.Play("DiceCombine");
                    }

                    //both were gold
                    else if (diceObjects[secondPressedNumber].GetComponent<Dice>().GetGoldStatus() &&
                        diceObjects[firstPressedNumber].GetComponent<Dice>().GetGoldStatus())
                    {
                        thirdDicePair[0] = firstPressedNumber;
                        thirdDicePair[1] = secondPressedNumber;

                        //Add die operations
                        diceNumbers[secondPressedNumber] += diceNumbers[firstPressedNumber];
                        diceObjects[firstPressedNumber].SetInactiveStatus(true);
                        diceObjects[firstPressedNumber].SetMarkedStatus(false);
                        diceObjects[secondPressedNumber].SetMarkedStatus(false);
                        diceImages[secondPressedNumber].sprite = diceSpritesPlatinum[diceNumbers[secondPressedNumber] - 1];

                        diceObjects[firstPressedNumber].SetGold(false, diceNumbers[firstPressedNumber]);
                        diceObjects[secondPressedNumber].SetGold(false, diceNumbers[secondPressedNumber]);
                        diceObjects[secondPressedNumber].SetPlatinum(true, diceNumbers[secondPressedNumber]);
                        //diceObjects[secondPressedNumber].SetMarkedStatus(true);
                        audioManager.Play("DiceCombine");
                    }

                    //If one of them was a pair, unmark it
                            else if (diceObjects[secondPressedNumber].GetComponent<Dice>().GetGoldStatus()
                        || diceObjects[firstPressedNumber].GetComponent<Dice>().GetGoldStatus() ||
                        diceObjects[secondPressedNumber].GetComponent<Dice>().GetPlatinumStatus()
                        || diceObjects[firstPressedNumber].GetComponent<Dice>().GetPlatinumStatus())

                    {
                        diceObjects[secondPressedNumber].SetMarkedStatus(false);
                    }
                }
            }
        }
    }

    public void OnKeyPressed()
    {
        DiceKey pressedDiceKey;
        var go = EventSystem.current.currentSelectedGameObject;
        if (go != null)
        {
            pressedDiceKey = go.GetComponent<DiceKey>();

            Dice firstPressedDice = null;
            int firstPressedNumber = 0;
            int secondPressedNumber = 0;

            //Find first marked die
            for (int i = 0; i < diceObjects.Count; i++)
            {
                if (diceObjects[i].GetMarkedStatus())
                {

                    firstPressedDice = diceObjects[i];
                    firstPressedNumber = diceNumbers[i];
                }
            }

            //If found
            if (firstPressedDice != null)
            {

                //Get the dicekey number
                foreach(var enemyGO in enemiesGO)
                {
                    if (enemyGO.GetComponent<EnemyBattleBase>().GetDiceKey() == pressedDiceKey)
                    {
                        secondPressedNumber = pressedDiceKey.GetDKNumber();
                        //secondPressedNumber = enemyGO.GetComponent<EnemyBattleBase>().GetDiceKeyNumber();
                        if (secondPressedNumber == firstPressedNumber && !pressedDiceKey.GetAssignedStatus() && !pressedDiceKey.GetInactiveStatus())
                        {
                            if(pressedDiceKey.GetGoldStatus() && !firstPressedDice.GetGoldStatus() && !firstPressedDice.GetPlatinumStatus())
                            {
                                break;
                                //Play negative sound
                            }
                            else if(pressedDiceKey.GetPlatinumStatus() && !firstPressedDice.GetPlatinumStatus())
                            {
                                break;
                                //Play negative sound
                            }
                            else
                            {
                                audioManager.Play("Assign");
                                firstPressedDice.SetAssignedStatus(true);
                                firstPressedDice.SetMarkedStatus(false);
                                //pressedDiceKey.SetAssignedStatus(true);
                                firstPressedDice.SetAssignedTo(pressedDiceKey.gameObject);
                                enemyGO.GetComponent<EnemyBattleBase>().Assign(true, secondPressedNumber);
                            }
                        }
                        else
                        {
                            //Play negative sound
                        }

                        break;
                    }
                }
            }
        }
    }

    public void PressedCancel()
    {
        if(state == BattleState.PLAYERTURN)
        {
            if (!cancelPressed)
            {
                cancelPressed = true;
                bool diceDeselected = false;
                var go = EventSystem.current.currentSelectedGameObject;

                for (int i = 0; i < diceObjects.Count; i++)
                {
                    if (diceObjects[i].GetMarkedStatus())
                    {
                        diceDeselected = true;
                        diceObjects[i].SetMarkedStatus(false);
                    }
                }

                //If no die was marked
                if (!diceDeselected)
                {
                    //If the selected dice is a dice key
                    if (go.GetComponent<DiceKey>() != null)
                    {
                        DiceKey pressedDiceKey = go.GetComponent<DiceKey>();

                        foreach (var enemyGO in enemiesGO)
                        {
                            if (enemyGO.GetComponent<EnemyBattleBase>().GetDiceKey() == pressedDiceKey)
                            {
                                enemyGO.GetComponent<EnemyBattleBase>().Assign(false, 0);

                                for (int k = 0; k < diceObjects.Count; k++)
                                {
                                    if (diceObjects[k].GetAssignedTo() == go)
                                    {
                                        diceObjects[k].SetAssignedStatus(false);
                                        diceObjects[k].SetAssignedTo(null);
                                        break;
                                    }
                                }
                                break;
                            }
                        }

                        //if the dicekey was assigned
                        if (pressedDiceKey.GetAssignedStatus())
                        {
                            pressedDiceKey.SetAssignedStatus(false, diceKeyNumbers[diceKeys.IndexOf(pressedDiceKey)]);

                            for (int i = 0; i < diceObjects.Count; i++)
                            {
                                if (diceObjects[i].GetAssignedTo() == pressedDiceKey.gameObject)
                                {
                                    //TODO
                                    Debug.Log("Hejsan");
                                }
                            }
                        }
                    }

                    //If the selected dice is a added one
                    else if (go.GetComponent<Dice>() != null)
                    {
                        Dice pressedDice = go.GetComponent<Dice>();

                        if (!pressedDice.GetInactiveStatus() && !pressedDice.GetAssignedStatus() && !pressedDice.GetLockedStatus())
                        {
                            if (go.GetComponent<Dice>().GetGoldStatus())
                            {
                                if (firstDicePair[1] != -1)
                                {
                                    if (pressedDice == diceObjects[firstDicePair[1]])
                                    {
                                        diceObjects[firstDicePair[1]].SetGold(false, -1);
                                        diceObjects[firstDicePair[0]].SetInactiveStatus(false);
                                        diceNumbers[firstDicePair[1]] -= diceNumbers[firstDicePair[0]];
                                        diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];

                                        firstDicePair[0] = -1;
                                        firstDicePair[1] = -1;
                                        buttonPanel.SetEmptyRedText();
                                    }
                                }

                                if (secondDicePair[1] != -1)
                                {
                                    //argument out of range, kan inte cancla nr 2
                                    if (pressedDice == diceObjects[secondDicePair[1]])
                                    {
                                        diceObjects[secondDicePair[1]].SetGold(false, -1);
                                        diceObjects[secondDicePair[0]].SetInactiveStatus(false);
                                        diceNumbers[secondDicePair[1]] -= diceNumbers[secondDicePair[0]];
                                        diceImages[secondDicePair[1]].sprite = diceSprites[diceNumbers[secondDicePair[1]] - 1];

                                        secondDicePair[0] = -1;
                                        secondDicePair[1] = -1;
                                        buttonPanel.SetEmptyRedText();
                                    }
                                }
                            }

                            else if (go.GetComponent<Dice>().GetPlatinumStatus())
                            {
                                if (thirdDicePair[1] != -1)
                                {
                                    if (pressedDice == diceObjects[thirdDicePair[1]])
                                    {
                                        diceObjects[thirdDicePair[1]].SetPlatinum(false, -1);
                                        diceObjects[thirdDicePair[0]].SetInactiveStatus(false);
                                        diceNumbers[thirdDicePair[1]] -= diceNumbers[thirdDicePair[0]];
                                        diceObjects[thirdDicePair[0]].SetGold(true, diceNumbers[thirdDicePair[0]]);
                                        diceObjects[thirdDicePair[1]].SetGold(true, diceNumbers[thirdDicePair[1]]);
                                        diceImages[thirdDicePair[1]].sprite = diceSpritesGold[diceNumbers[thirdDicePair[1]] - 1];

                                        thirdDicePair[0] = -1;
                                        thirdDicePair[1] = -1;
                                        buttonPanel.SetSplitText();
                                    }
                                }
                            }
                        }
                    }

                    else if(go.GetComponent<BattleAbilityHolder>() != null)
                    {
                        BattleAbilityHolder holder = go.GetComponent<BattleAbilityHolder>();

                        if(holder.GetMarkedStatus())
                        {
                            holder.AbilityClicked();
                        }

                    }
                }
            }

            if (cancelPressed)
            {
                cancelPressed = false;
            }
        }
    }

    public int GetDiceNumber(Dice dice)
    {
        return diceNumbers[diceObjects.IndexOf(dice)];
    }

    public int GetNumberOfEnemies()
    {
        return enemiesGO.Count;
    }

    public int GetEnemyIndex(GameObject enemy)
    {
        return enemiesGO.IndexOf(enemy);
    }

    public bool CheckDiceSelected()
    {
        for (int i = 0; i < diceObjects.Count; i++)
        {
            if (diceObjects[i].GetMarkedStatus())
            {
                return true;
            }
        }

        return false;
    }

    private void PreventCursor()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

    IEnumerator WaitSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void OnAssignButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(EnemyTurn());
    }

    //This is for the cmd cheat to kill all enemies
    public void KillAllEnemies()
    {
        foreach (var enemyGO in enemiesGO)
        {
            enemyGO.GetComponent<EnemyBattleBase>().Assign(true, 1);
        }
        StartCoroutine(EnemyTurn());
    }
}
