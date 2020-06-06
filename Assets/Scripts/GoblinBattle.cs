using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoblinBattle : EnemyBattleBase
{
    [SerializeField] ThrowSimulation rockToThrow;
    public Transform throwingHand;
    // Start is called before the first frame update
    void Awake()
    {
        diceKeyNumber = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EnemySetup()
    {

    }

    public override void EnemyAction()
    {
        var rock = Instantiate(rockToThrow, throwingHand);
        rock.SetTarget(FindObjectOfType<PlayerBattleController>().playerBody);
        var rock2 = Instantiate(rockToThrow, throwingHand);
        rock2.SetTarget(FindObjectOfType<PlayerBattleController>().playerHead);
    }
}
