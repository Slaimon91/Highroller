using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    public int Dice1;
    public int Dice2;
    public int Dice3;
    public int TestDice;
    public int nrOf0;
    public int nrOf1;
    public int nrOf2;
    public int nrOf3;
    public int nrOf4;
    public int nrOf5;
    public int nrOf6;
    public int nrOf7;


    public Image[] diceImages;

    public Sprite[] diceSprites;

    EnemyBattle enemyUnit;
    PlayerBattleController player;

    [SerializeField] TextMeshProUGUI enemyNameText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetUpBattle();
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
        Dice1 = Random.Range(1, 13);
        diceImages[0].sprite = diceSprites[Dice1 - 1];
        Dice2 = Random.Range(1, 13);
        diceImages[1].sprite = diceSprites[Dice2 - 1];
        Dice3 = Random.Range(1, 13);
        diceImages[2].sprite = diceSprites[Dice3 - 1];

        for (int i = 0; i < 10000; i++)
        {
            TestDice = Random.Range(1, 7);
            switch(TestDice)
            {
                case 0:
                    nrOf0++;
                    break;
                case 1:
                    nrOf1++;
                    break;
                case 2:
                    nrOf2++;
                    break;
                case 3:
                    nrOf3++;
                    break;
                case 4:
                    nrOf4++;
                    break;
                case 5:
                    nrOf5++;
                    break;
                case 6:
                    nrOf6++;
                    break;
                case 7:
                    nrOf7++;
                    break;
            }
        }
        Debug.Log("0: " + nrOf0);
        Debug.Log("1: " + nrOf1);
        Debug.Log("2: " + nrOf2);
        Debug.Log("3: " + nrOf3);
        Debug.Log("4: " + nrOf4);
        Debug.Log("5: " + nrOf5);
        Debug.Log("6: " + nrOf6);
        Debug.Log("7: " + nrOf7 + "\n\n");
    }

    void EnemyTurn()
    {
        PlayerTurn();
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        EnemyTurn();
    }

}
