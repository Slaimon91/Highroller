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
    public BattleState state;

    public GameObject playerPrefab;
    private List<GameObject> enemyPrefabs = new List<GameObject>();
    private List<GameObject> enemiesGO = new List<GameObject>();
    [SerializeField] GameObject[] enemiesInfo;
    private GameObject lastselect;
    private PlayerBattleController player;
    private bool cancelPressed = false;

    public Transform playerSpawnPoint;
    public Transform[] enemySpawnPoints;

    private int[] diceNumbers;
    public Image[] diceImages;
    public Sprite[] diceSprites;
    public Dice[] diceGameObjects;
    private int[] firstDicePair = {-1, -1 };
    private int[] secondDicePair = {-1, -1 };

    public int[] diceKeyNumbers;
    public Image[] diceKeyImages;
    public DiceKey[] diceKeys;

    int nrOfEnemies;

    PlayerControls controls;
    [SerializeField] BattleStartupInfo battleStartupInfo;
    private Sprite battleBackground;

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
        nrOfEnemies = battleStartupInfo.enemies.Count;
        for(int i = 0; i < nrOfEnemies; i++)
        {
            enemyPrefabs.Add(battleStartupInfo.enemies[i]);
            var enemyGO = Instantiate(enemyPrefabs[i], enemySpawnPoints[i]);
            enemiesGO.Add(enemyGO);
            //diceKeys[i].transform.parent.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = enemyGO.GetComponent<EEnemyInterface>().GetUnitName();
            diceKeyNumbers[i] = enemyGO.GetComponent<EEnemyInterface>().GetDiceKeyNumber();
            diceKeyImages[i].sprite = diceSprites[diceKeyNumbers[i] - 1];
            enemyGO.GetComponent<EEnemyInterface>().SetDiceKeyGO(diceKeys[i]);
            enemiesInfo[i].SetActive(true);
        }
        GetComponentInChildren<SpriteRenderer>().sprite = battleStartupInfo.battleBackground;
        player = playerGO.GetComponent<PlayerBattleController>();
        SetupButtonNavigation();

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void SetupButtonNavigation()
    {
        
        if (battleStartupInfo.nrOfDices == 3)
        {
            diceNumbers = new int[] { 0, 0, 0 };
            diceGameObjects[3].gameObject.SetActive(false);
            diceGameObjects[4].gameObject.SetActive(false);
            diceKeys[3].SetButtonNavigation(diceGameObjects[2].GetComponent<Button>(), "up");
        }
        else if (battleStartupInfo.nrOfDices == 4)
        {
            diceNumbers = new int[] { 0, 0, 0, 0 };
            diceGameObjects[4].gameObject.SetActive(false);
        }
        else if (battleStartupInfo.nrOfDices == 5)
        {
            diceNumbers = new int[] { 0, 0, 0, 0, 0 };
        }

        /*if(battleStartupInfo.enemies.Count == 1)
        {
            diceKeys[1].gameObject.SetActive(false);
            diceKeys[2].gameObject.SetActive(false);
            diceKeys[3].gameObject.SetActive(false);
        }
        else if (battleStartupInfo.enemies.Count == 2)
        {
            diceKeys[2].gameObject.SetActive(false);
            diceKeys[3].gameObject.SetActive(false);
        }
        else if (battleStartupInfo.enemies.Count == 3)
        {
            diceKeys[3].gameObject.SetActive(false);
        }*/

    }

    void PlayerTurn()
    {
        for (int i = 0; i < diceNumbers.Length; i++)
        {
            if(diceGameObjects[i].GetAssignedStatus())
            {
                diceGameObjects[i].SetAssignedStatus(false);
                diceGameObjects[i].SetAssignedTo(null);
            }

            if(!diceGameObjects[i].GetLockedOrInactiveStatus() && !diceGameObjects[i].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
            {
                diceNumbers[i] = Random.Range(1, 7);
                diceImages[i].sprite = diceSprites[diceNumbers[i] - 1];
            }
            else if(!diceGameObjects[i].GetLockedOrInactiveStatus() && diceGameObjects[i].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
            {
                Dice pressedDice = diceGameObjects[i].GetComponent<Dice>();

                if (pressedDice == diceGameObjects[firstDicePair[1]])
                {
                    diceGameObjects[firstDicePair[0]].SetInactiveStatus(false);
                    diceNumbers[firstDicePair[1]] -= diceNumbers[firstDicePair[0]];
                    diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                    diceNumbers[firstDicePair[0]] = Random.Range(1, 7);
                    diceNumbers[firstDicePair[1]] = Random.Range(1, 7);
                    diceImages[firstDicePair[0]].sprite = diceSprites[diceNumbers[firstDicePair[0]] - 1];
                    diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                    firstDicePair[0] = -1;
                    firstDicePair[1] = -1;
                }
                else if (pressedDice == diceGameObjects[secondDicePair[1]])
                {
                    diceGameObjects[secondDicePair[0]].SetInactiveStatus(false);
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
            if(diceGameObjects[i].GetLockStatus())
            {
                diceGameObjects[i].ToggleLockDice();
            }
            diceGameObjects[i].SetMarkedStatus(false);
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
            secondPressedDice = go.GetComponent<Dice>();
            if(!secondPressedDice.GetLockedOrInactiveStatus())
            {
                Dice firstPressedDice = null;
                int firstPressedNumber = 0;
                int secondPressedNumber = 0;

                for (int i = 0; i < diceNumbers.Length; i++)
                {
                    if (diceGameObjects[i].GetMarkedStatus())
                    {
                        if (diceGameObjects[i] != secondPressedDice)
                        {
                            firstPressedDice = diceGameObjects[i];
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
                        diceGameObjects[firstPressedNumber].SetInactiveStatus(true);
                        diceGameObjects[firstPressedNumber].SetMarkedStatus(false);
                        diceGameObjects[secondPressedNumber].SetMarkedStatus(false);
                    }
                    //If one of them was a pair, unmark it
                    else if (diceImages[secondPressedNumber].GetComponent<Image>().sprite.name.StartsWith("Added_Dice") 
                        || diceImages[firstPressedNumber].GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
                    {
                        diceGameObjects[secondPressedNumber].SetMarkedStatus(false);
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
            for (int i = 0; i < diceNumbers.Length; i++)
            {
                if (diceGameObjects[i].GetMarkedStatus())
                {

                    firstPressedDice = diceGameObjects[i];
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

            for (int i = 0; i < diceNumbers.Length; i++)
            {
                if(diceGameObjects[i].GetMarkedStatus())
                {
                    diceDeselected = true;
                    diceGameObjects[i].SetMarkedStatus(false);
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

                            for (int k = 0; k < diceGameObjects.Length; k++)
                            {
                                if(diceGameObjects[k].GetAssignedTo() == go)
                                {
                                    diceGameObjects[k].SetAssignedStatus(false);
                                    diceGameObjects[k].SetAssignedTo(null);
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
                            if(diceGameObjects[i].GetAssignedTo() == pressedDiceKey.gameObject)
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
                        if (pressedDice == diceGameObjects[firstDicePair[1]])
                        {
                            diceGameObjects[firstDicePair[0]].SetInactiveStatus(false);
                            diceNumbers[firstDicePair[1]] -= diceNumbers[firstDicePair[0]];
                            diceImages[firstDicePair[1]].sprite = diceSprites[diceNumbers[firstDicePair[1]] - 1];
                            firstDicePair[0] = -1;
                            firstDicePair[1] = -1;
                        }
                        else if (pressedDice == diceGameObjects[secondDicePair[1]])
                        {
                            diceGameObjects[secondDicePair[0]].SetInactiveStatus(false);
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
