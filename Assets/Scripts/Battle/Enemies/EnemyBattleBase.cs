using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBattleBase : MonoBehaviour
{
    protected int diceKeyNumber;
    [SerializeField] protected string unitName;
    protected DiceKey diceKeyGO;
    [SerializeField] protected Sprite icon;
    protected Image diceKeyImage;
    protected bool isDead = false;
    protected bool isAssigned = false;
    [SerializeField] protected int damageAmount = 0;

    [SerializeField]
    [TextArea(3, 20)]
    protected string infoText;

    protected Animator animator;

    public abstract void EnemySetup();

    public void Assign(bool status)
    {
        if (!status)
        {
            isAssigned = false;

            diceKeyGO.SetAssignedStatus(false, diceKeyNumber);
        }
        else
        {
            isAssigned = true;

            diceKeyGO.SetAssignedStatus(true, diceKeyNumber);
        }
    }

    public abstract IEnumerator EnemyAction();

    public virtual void TriggerDying()
    {
        isDead = true;
        if (isDead)
        {
            //Destroy(diceKeyGO.gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void Die()
    {
        
    }

    public string GetInfoText()
    {
        return infoText;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public bool GetAssignedStatus()
    {
        return isAssigned;
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

    public int GetDamageAmount()
    {
        return damageAmount;
    }

    public void SetDiceKeyGO(DiceKey diceKey)
    {
        diceKeyGO = diceKey;
        diceKeyImage = diceKeyGO.GetComponent<Image>();
    }
}
