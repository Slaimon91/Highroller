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
    protected bool isFrontDKAssigned = false;

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
    [SerializeField] protected List<MoreDices> moreDices = new List<MoreDices>();

    [SerializeField]
    [TextArea(3, 20)]
    protected string infoText;

    protected Animator animator;

    //Happens after attacks before it becomes the players turn again
    public virtual IEnumerator EnemySetup()
    {
        yield return null;
    }

    //Assign the monster because the DK was matched
    public virtual void Assign(bool status, int number)
    {
        if (!status)
        {
            isAssigned = false;
            isFrontDKAssigned = false;

            diceKeyGO.SetAssignedStatus(false, number);
        }
        else
        {
            //If the monster does not have any more dices
            if(!diceKeyGO.TestMoreDices())
            {
                isAssigned = true;
                diceKeyGO.SetAssignedStatus(true, number);
            }
            else
            {
                isFrontDKAssigned = true;
                diceKeyGO.SetAssignedStatus(true, number);
            }
        }
    }

    //Enemy attack phase
    public abstract IEnumerator EnemyAction();

    //Player enemy death animation
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

    //Actually die
    public virtual IEnumerator Die()
    {
        yield return null;
    }

    //If the enemy collides with the player
    public virtual void CollideWithPlayer()
    {

    }

    //See if the enemy will drop a soul
    public void RollSoulDrop()
    {
        int num = Random.Range(1, 101);
        if(num <= soulDropPercentage)
        {
            FindObjectOfType<BattleBounty>().AddSoul(unitName);
        }
    }

    //Setup if it's a boss with extra dices
    public void ExtraDiceSetup()
    {
        List<int> diceNumbers = new List<int>();
        List<string> diceStatuses = new List<string>();

        for (int i = 0; i < moreDices.Count; i++)
        {
            diceNumbers.Add(moreDices[i].diceKeyNumber);

            if (moreDices[i].isGold)
            {
                diceStatuses.Add("gold");
            }

            else if (moreDices[i].isPlatinum)
            {
                diceStatuses.Add("plat");
            }

            else if (moreDices[i].isInactive)
            {
                diceStatuses.Add("deactivated");
            }

            else
            {
                diceStatuses.Add("normal");
            }
        }

        diceKeyGO.SetupMoreDices(diceStatuses, diceNumbers);
    }

    //Will activate the next DK in line if it's a boss monster
    public IEnumerator ActivateNextDK()
    {
        diceKeyGO.ActivateNextDK();

        isFrontDKAssigned = false;

        yield return StartCoroutine(NextDKWasActivated());
    }

    public virtual IEnumerator NextDKWasActivated()
    {
        yield return null;
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

    public bool GetFrontDKAssignedStatus()
    {
        return isFrontDKAssigned;
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

[System.Serializable]
public class MoreDices
{
    public int diceKeyNumber = 1;
    public bool isGold = false;
    public bool isPlatinum = false;
    public bool isInactive = false;
}
