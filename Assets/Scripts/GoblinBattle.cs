using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoblinBattle : MonoBehaviour, EEnemyInterface
{
    [SerializeField] int diceKeyNumber;
    [SerializeField] string unitName;
    [SerializeField] DiceKey diceKeyGO;
    [SerializeField] Sprite icon;
    private Image diceKeyImage;
    private bool isDead = false;

    // Start is called before the first frame update
    void Awake()
    {
        diceKeyNumber = 3;
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
        if(!status)
        {
            isDead = false;

            diceKeyGO.SetAssignedStatus(false);
        }
        else
        {
            isDead = true;

            diceKeyGO.SetAssignedStatus(true);
        }
    }

    public void EnemyAction()
    {
        if (isDead)
        {
            Destroy(diceKeyGO.transform.parent.gameObject);
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
