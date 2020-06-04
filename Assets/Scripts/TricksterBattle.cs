using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TricksterBattle : MonoBehaviour, EEnemyInterface
{
    [SerializeField] int diceKeyNumber;
    [SerializeField] string unitName;
    [SerializeField] DiceKey diceKeyGO;
    [SerializeField] Sprite icon;
    private Image diceKeyImage;
    private BattleSystem battleSystem;
    private bool isDead = false;
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

    public void EnemySetup()
    {

    }

    public void Assign(bool status)
    {
        if (!status)
        {
            isDead = false;

            diceKeyGO.SetAssignedStatus(false, diceKeyNumber);
        }
        else
        {
            isDead = true;

            diceKeyGO.SetAssignedStatus(true, diceKeyNumber);
        }
    }

    public void EnemyAction()
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
        
    }

    public void TriggerDeath()
    {
        if (isDead)
        {
            //Destroy(diceKeyGO.gameObject);
            Destroy(gameObject);
        }
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public bool GetDeathStatus()
    {
        return isDead;
    }

    public string GetUnitName()
    {
        return unitName;
    }
    public int GetDiceKeyNumber()
    {
        return diceKeyNumber;
    }

    public DiceKey GetDiceKey()
    {
        return diceKeyGO;
    }

    public void SetDiceKeyGO(DiceKey diceKey)
    {
        diceKeyGO = diceKey;
        diceKeyImage = diceKeyGO.GetComponent<Image>();
    }
}
