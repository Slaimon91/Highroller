using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TricksterBattle : EnemyBattleBase
{
    private BattleSystem battleSystem;
    // Start is called before the first frame update
    void Awake()
    {
        diceKeyNumber = Random.Range(1, 7);
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void EnemySetup()
    {

    }

    public override IEnumerator EnemyAction()
    {
        int newDiceKeyNumber = diceKeyNumber;
        while(newDiceKeyNumber == diceKeyNumber)
        {
            newDiceKeyNumber = Random.Range(1, 7);
        }
        diceKeyNumber = newDiceKeyNumber;
        if(diceKeyGO != null)
        {
           diceKeyImage.sprite = battleSystem.diceSprites[diceKeyNumber - 1];
        }
        yield return new WaitForSeconds(2);
    }
}
