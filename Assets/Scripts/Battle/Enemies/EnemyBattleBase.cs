using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBattleBase : MonoBehaviour
{
    protected DiceKey diceKeyGO;
    protected Image diceKeyImage;
    protected bool isDead = false;
    protected bool isAssigned = false;

    [SerializeField] protected string unitName = "Name";
    [SerializeField] protected int diceKeyNumber = 1;
    [SerializeField] protected int xpAmount = 1;
    [SerializeField] protected bool isGold = false;
    [SerializeField] protected bool isPlatinum = false;
    [SerializeField] protected bool isInactive = false;
    [SerializeField] protected int damageAmount = 0;
    [SerializeField] protected int soulDropPercentage = 0;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected int preferedSpawnLocation = -1;

    [SerializeField]
    [TextArea(3, 20)]
    protected string infoText;

    protected Animator animator;

    //Enemy actions
    public virtual IEnumerator EnemySetup()
    {
        yield return null;
    }

    public virtual void Assign(bool status)
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
        FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        isDead = true;
        if (isDead)
        {
            //Destroy(diceKeyGO.gameObject);
            Destroy(gameObject);
        }
    }

    public virtual IEnumerator Die()
    {
        yield return null;
    }

    public virtual void CollideWithPlayer()
    {

    }

    public void RollSoulDrop()
    {
        int num = Random.Range(1, 101);
        if(num <= soulDropPercentage)
        {
            FindObjectOfType<BattleBounty>().AddSoul(unitName);
        }
    }

    //Getters & Setters

    public int GetPreferedSpawnLocation()
    {
        return preferedSpawnLocation;
    }
    public int GetXPAmount()
    {
        return xpAmount;
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

    public bool GetGoldStatus()
    {
        return isGold;
    }

    public bool GetPlatinumStatus()
    {
        return isPlatinum;
    }

    public bool GetInactiveStatus()
    {
        return isInactive;
    }

    public int GetSoulDropPercentage()
    {
        return soulDropPercentage;
    }

    public void SetSoulDropPercentage(int percentage)
    {
        soulDropPercentage = percentage;
    }

    public void SetDiceKeyGO(DiceKey diceKey)
    {
        diceKeyGO = diceKey;
        diceKeyImage = diceKeyGO.GetComponent<Image>();
    }

    public void SetDamageAmount(int amount)
    {
        damageAmount = amount;
    }
}
