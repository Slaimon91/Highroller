using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    private GameObject lastselect;
    private bool cancelPressed = false;

    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    public int[] diceNumbers;
    public Image[] diceImages;
    public Sprite[] diceSprites;
    public Dice[] diceGameObjects;
    private int[] firstDicePair = {-1, -1 };
    private int[] secondDicePair = {-1, -1 };

    EnemyBattle enemyUnit;
    PlayerBattleController player;

    [SerializeField] TextMeshProUGUI enemyNameText;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetUpBattle();
    }

    void Update()
    {
        PreventCursor();
        PressCancel();
    }

    void SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawnPoint);
        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawnPoint);
        enemyUnit = enemyGO.GetComponent<EnemyBattle>();

        enemyNameText.text = enemyUnit.unitName;

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        for (int i = 0; i < diceNumbers.Length; i++)
        {
            if(diceGameObjects[i].RerollDieStatus())
            {
                diceNumbers[i] = Random.Range(1, 7);
                diceImages[i].sprite = diceSprites[diceNumbers[i] - 1];
            }
            if(diceGameObjects[i].GetLockStatus())
            {
                diceGameObjects[i].ToggleLockDice();
            }
        }
    }

    void EnemyTurn()
    {
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

    private void PressCancel()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel") && !cancelPressed)
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
                //If the selected dice is a added one
                if (go.GetComponent<Image>().sprite.name.StartsWith("Added_Dice"))
                {
                    Dice pressedDice = go.GetComponent<Dice>();

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

        if (CrossPlatformInputManager.GetButtonDown("Cancel") && cancelPressed)
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
}
