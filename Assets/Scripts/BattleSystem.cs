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
    }

    void EnemyTurn()
    {
        PlayerTurn();
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
