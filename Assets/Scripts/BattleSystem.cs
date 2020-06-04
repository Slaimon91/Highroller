using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.InputSystem;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    //Enemies and abilities
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    private List<GameObject> enemiesGO = new List<GameObject>();
    private List<EnemyInfo> enemiesInfo = new List<EnemyInfo>();
    private List<BattleAbility> abilities = new List<BattleAbility>();

    //Spawnpoints
    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    //Dice and dice keys
    private int[] diceNumbers;
    private List<Image> diceImages = new List<Image>();
    public Sprite[] diceSprites;
    private List<Dice> diceObjects = new List<Dice>();
    private int[] firstDicePair = { -1, -1 };
    private int[] secondDicePair = { -1, -1 };

    private List<int> diceKeyNumbers = new List<int>();
    private List<Image> diceKeyImages = new List<Image>();
    private List<DiceKey> diceKeys = new List<DiceKey>();

    //Prefabs and panels
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Dice dicePrefabs;
    [SerializeField] DiceKey diceKeyPrefab;
    [SerializeField] EnemyInfo enemiesInfoPrefab;
    [SerializeField] BattleAbility abilitiesPrefab;
    [SerializeField] BattleAbility abilitiesPrefabThree;
    [SerializeField] Transform dicePanel;
    [SerializeField] Transform diceKeyPanel;
    [SerializeField] Transform enemiesInfoPanel;
    [SerializeField] Transform abilitiesPanel;
    [SerializeField] Transform abilitiesPanelThree;
    [SerializeField] Sprite threeAbilitiesSprite;
    [SerializeField] BattleStartupInfo battleStartupInfo;

    public BattleState state;
    int nrOfEnemies;
    PlayerControls controls;
    private GameObject lastselect;
    private PlayerBattleController player;
    private bool cancelPressed = false;
    private Sprite battleBackground;
    private EventSystem eventSystem;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Cancel.performed += ctx => PressedCancel();
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        state = BattleState.START;
        eventSystem = FindObjectOfType<EventSystem>();
        SetUpBattle();
    }

    void Update()
    {
        //PreventCursor();
        //PressCancel();
        //OnDicePressed();
    }

    void SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawnPoint);
        nrOfEnemies = battleStartupInfo.enemies.Count;
        InstantiateDices();
        for (int i = 0; i < nrOfEnemies; i++)
        {
            enemyPrefabs.Add(battleStartupInfo.enemies[i]);
            var enemyGO = Instantiate(enemyPrefabs[i], enemySpawnPoints[i]);
            enemiesGO.Add(enemyGO);
            diceKeyNumbers.Add(enemyGO.GetComponent<EEnemyInterface>().GetDiceKeyNumber());
            diceKeyImages[i].sprite = diceSprites[diceKeyNumbers[i] - 1];
            enemyGO.GetComponent<EEnemyInterface>().SetDiceKeyGO(diceKeys[i]);
        }
        GetComponentInChildren<SpriteRenderer>().sprite = battleStartupInfo.battleBackground;
        player = playerGO.GetComponent<PlayerBattleController>();
        SetupButtonNavigation();

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void InstantiateDices()
    {
        //Instantiate dices
        if (battleStartupInfo.nrOfDices == 3)
        {
            int offset = 46;
            for(int i = 0; i < battleStartupInfo.nrOfDices; i++)
            {
                diceObjects.Add(Instantiate(dicePrefabs, dicePanel.transform));
                RectTransform rt = diceObjects[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(46 - offset*i, 0, 0);
                diceImages.Add(diceObjects[i].GetComponent<Image>());
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
            }

            diceNumbers = new int[] { 0, 0, 0, 0 };
        }
        else if (battleStartupInfo.nrOfDices == 5)
        {
            int offset = 31;
            for (int i = 0; i < battleStartupInfo.nrOfDices; i++)
            {
                diceObjects.Add(Instantiate(dicePrefabs, dicePanel.transform));
                RectTransform rt = diceObjects[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(62 - offset * i, 0, 0);
                diceImages.Add(diceObjects[i].GetComponent<Image>());
            }

            diceNumbers = new int[] { 0, 0, 0, 0, 0 };
        }

        eventSystem.firstSelectedGameObject = diceObjects[0].gameObject;

        //Instantiate enemies and dicekeys
        for(int i = 0; i < battleStartupInfo.enemies.Count; i++)
        {
            int offset = 31;
            int offsetInfo = 22;
            diceKeys.Add(Instantiate(diceKeyPrefab, diceKeyPanel.transform));
            RectTransform rt = diceKeys[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0 - offset * i, 0, 0);
            diceKeyImages.Add(diceKeys[i].GetComponent<Image>());

            enemiesInfo.Add(Instantiate(enemiesInfoPrefab, enemiesInfoPanel.transform));
            RectTransform rtInfo = enemiesInfo[i].GetComponent<RectTransform>();
            rtInfo.anchoredPosition = new Vector3(0 - offsetInfo * i, 0, 0);
            Image enemiesInfoImage = enemiesInfo[i].GetComponent<Image>();
            enemiesInfoImage.sprite = battleStartupInfo.enemies[i].GetComponent<EEnemyInterface>().GetIcon();
        }

        //Instantiate player abilities
        for (int i = 0; i < battleStartupInfo.abilities.Count; i++)
        {
            if(battleStartupInfo.abilities.Count == 1 || battleStartupInfo.abilities.Count == 2)
            {
                int offset = 22;

                abilities.Add(Instantiate(abilitiesPrefab, abilitiesPanel.transform));
                RectTransform rt = abilities[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(0 + offset * i, 0, 0);
                Image abilitiesImage = abilities[i].GetComponent<Image>();
                abilitiesImage.sprite = battleStartupInfo.abilities[i].GetComponent<SpriteRenderer>().sprite;
            }

            else if(battleStartupInfo.abilities.Count == 3)
            {
                abilitiesPanel.gameObject.SetActive(false);
                abilitiesPanelThree.gameObject.SetActive(true);

                int offset = 22;

                abilities.Add(Instantiate(abilitiesPrefabThree, abilitiesPanelThree.transform));
                RectTransform rt = abilities[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(0 + offset * i, 0, 0);
                Image abilitiesImage = abilities[i].GetComponent<Image>();
                abilitiesImage.sprite = battleStartupInfo.abilities[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    void SetupButtonNavigation()
    {
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

        /*float distance = Vector3.Distance(Camera.main.ViewportToWorldPoint(diceKeys[i].GetComponent<RectTransform>().position),
                                        Camera.main.ViewportToWorldPoint(enemiesInfo[k].GetComponent<RectTransform>().position));
Debug.Log("Distance from key " + i + " to enemy " + k + " is = " + distance + " where the key is at " +
    Camera.main.ViewportToWorldPoint(diceKeys[i].GetComponent<RectTransform>().position) + " and the enemy is at " +
        Camera.main.ViewportToWorldPoint(enemiesInfo[k].GetComponent<RectTransform>().position));
if (distance < shortestDistanceInfoUnits || k == 0)
{
    shortestDistanceInfo = k;
    shortestDistanceInfoUnits = distance;
}*/

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
                if(abilities.Count > 0)
                {
                    enemiesInfo[enemiesInfo.Count - 1].SetButtonNavigation(abilities[abilities.Count - 1].GetComponent<Button>(), "left");
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
        for (int i = 0; i < abilities.Count; i++)
        {
            if (i + 1 < abilities.Count)
            {
                abilities[i].SetButtonNavigation(abilities[i + 1].GetComponent<Button>(), "right");
            }

            if (i != 0)
            {
                abilities[i].SetButtonNavigation(abilities[i - 1].GetComponent<Button>(), "left");
            }

            if (i == abilities.Count - 1)
            {
                if (enemiesInfo.Count > 0)
                {
                    abilities[abilities.Count - 1].SetButtonNavigation(enemiesInfo[enemiesInfo.Count - 1].GetComponent<Button>(), "right");
                }
            }

            abilities[i].SetButtonNavigation(diceKeys[diceKeys.Count - 1].GetComponent<Button>(), "down");
        }

    }

    void PlayerTurn()
    {
        Debug.Log(diceObjects.Count);
        OnDicePressed();
        for (int i = 0; i < diceObjects.Count; i++)
        {
            if(diceObjects[i].GetAssignedStatus())
            {
                diceObjects[i].SetAssignedStatus(false);
                diceObjects[i].SetAssignedTo(null);
            }

            if(!diceObjects[i].GetLockedOrInactiveStatus() && !diceObjects[i].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
            {
                diceNumbers[i] = Random.Range(1, 7);
                diceImages[i].sprite = diceSprites[diceNumbers[i] - 1];
            }
            else if(!diceObjects[i].GetLockedOrInactiveStatus() && diceObjects[i].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
            {
                Dice pressedDice = diceObjects[i].GetComponent<Dice>();

                if (pressedDice == diceObjects[firstDicePair[1]])
                {
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
                else if (pressedDice == diceObjects[secondDicePair[1]])
                {
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
            if(diceObjects[i].GetLockStatus())
            {
                diceObjects[i].ToggleLockDice();
            }
            diceObjects[i].SetMarkedStatus(false);
        }
    }

    void EnemyTurn()
    {
        player.TakeDamage(1);

        List<GameObject> toRemoveList = new List<GameObject>();

        //see if someone will die
        foreach(var enemyGO in enemiesGO)
        {
            if (enemyGO.GetComponent<EEnemyInterface>().GetDeathStatus())
            {
                toRemoveList.Add(enemyGO);
            }
            else
            {
                enemyGO.GetComponent<EEnemyInterface>().EnemyAction();
            }
        }

        //Actually kill them
        foreach(var toRemoveGO in toRemoveList)
        {
            toRemoveGO.GetComponent<EEnemyInterface>().EnemyAction();
            enemiesGO.Remove(toRemoveGO);
            nrOfEnemies--;
        }

        if(enemiesGO.Count == 0)
        {
            FindObjectOfType<LevelLoader>().LoadOverworldScene();
        }

        PlayerTurn();
    }

    public void OnDicePressed()
    {
        Dice secondPressedDice;
        var go = EventSystem.current.currentSelectedGameObject;
        if(go != null)
        {
            Debug.Log(diceObjects.Count);
            secondPressedDice = go.GetComponent<Dice>();
            if(!secondPressedDice.GetLockedOrInactiveStatus())
            {
                Debug.Log(diceObjects.Count);
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
                    if (!diceImages[secondPressedNumber].GetComponent<Image>().sprite.name.StartsWith("Added_Dice") && 
                        !diceImages[firstPressedNumber].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
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
                        diceImages[secondPressedNumber].sprite = diceSprites[diceNumbers[secondPressedNumber] + 12 - 1];
                        diceObjects[firstPressedNumber].SetInactiveStatus(true);
                        diceObjects[firstPressedNumber].SetMarkedStatus(false);
                        diceObjects[secondPressedNumber].SetMarkedStatus(false);
                    }
                    //If one of them was a pair, unmark it
                    else if (diceImages[secondPressedNumber].GetComponent<Image>().sprite.name.StartsWith("Added_Dice") 
                        || diceImages[firstPressedNumber].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
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
                    if(enemyGO.GetComponent<EEnemyInterface>().GetDiceKey() == pressedDiceKey)
                    {
                        secondPressedNumber = enemyGO.GetComponent<EEnemyInterface>().GetDiceKeyNumber();
                        if (secondPressedNumber == firstPressedNumber)
                        {
                            firstPressedDice.SetAssignedStatus(true);
                            firstPressedDice.SetMarkedStatus(false);
                            //pressedDiceKey.SetAssignedStatus(true);
                            firstPressedDice.SetAssignedTo(pressedDiceKey.gameObject);
                            enemyGO.GetComponent<EEnemyInterface>().Assign(true);
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

    private void PressedCancel()
    {
        if (!cancelPressed)
        {
            cancelPressed = true;
            bool diceDeselected = false;
            var go = EventSystem.current.currentSelectedGameObject;

            for (int i = 0; i < diceObjects.Count; i++)
            {
                if(diceObjects[i].GetMarkedStatus())
                {
                    diceDeselected = true;
                    diceObjects[i].SetMarkedStatus(false);
                }
            }

            //If no die was marked
            if(!diceDeselected)
            {
                //If the selected dice is a dice key
                if(go.GetComponent<DiceKey>())
                {
                    DiceKey pressedDiceKey = go.GetComponent<DiceKey>();

                    foreach (var enemyGO in enemiesGO)
                    {
                        if (enemyGO.GetComponent<EEnemyInterface>().GetDiceKey() == pressedDiceKey)
                        {
                            enemyGO.GetComponent<EEnemyInterface>().Assign(false);

                            for (int k = 0; k < diceObjects.Count; k++)
                            {
                                if(diceObjects[k].GetAssignedTo() == go)
                                {
                                    diceObjects[k].SetAssignedStatus(false);
                                    diceObjects[k].SetAssignedTo(null);
                                    break;
                                } 
                            }
                            break;
                        }
                    }



                    if (pressedDiceKey.GetAssignedStatus())
                    {
                        pressedDiceKey.SetAssignedStatus(false);

                        for (int i = 0; i < diceNumbers.Length; i++)
                        {
                            if(diceObjects[i].GetAssignedTo() == pressedDiceKey.gameObject)
                            {
                                //TODO
                            }
                        }
                    }
                }

                //If the selected dice is a added one
                else if (go.GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
                {
                    Dice pressedDice = go.GetComponent<Dice>();

                    if(!pressedDice.GetInactiveStatus() && !pressedDice.GetAssignedStatus())
                    {
                        if (pressedDice == diceObjects[firstDicePair[1]])
                        {
                            diceObjects[firstDicePair[0]].SetInactiveStatus(false);
                            diceNumbers[firstDicePair[1]] -= diceNumbers[firstDicePair[0]];
                            diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                            firstDicePair[0] = -1;
                            firstDicePair[1] = -1;
                        }
                        else if (pressedDice == diceObjects[secondDicePair[1]])
                        {
                            diceObjects[secondDicePair[0]].SetInactiveStatus(false);
                            diceNumbers[secondDicePair[1]] -= diceNumbers[secondDicePair[0]];
                            diceImages[secondDicePair[1]].sprite = diceSprites[diceNumbers[secondDicePair[1]] - 1];
                            secondDicePair[0] = -1;
                            secondDicePair[1] = -1;
                        }
                    }
                }
            }
        }

        if (cancelPressed)
        {
            cancelPressed = false;
        }
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

    public void OnAssignButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        EnemyTurn();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
