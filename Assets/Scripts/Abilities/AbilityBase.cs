using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBase : MonoBehaviour
{
    [SerializeField] protected string abilityName;

    [SerializeField]
    [TextArea(3, 20)]
    protected string info;

    protected GameObject battleImageHolder;
    protected GameObject inventoryImageHolder;
    [SerializeField] protected bool activeAbility = false;

    protected bool inactive = false;

    [SerializeField] protected int inventorySlotNr;

    void Awake()
    {
        transform.SetSiblingIndex(0);
        if(GetComponentInChildren<BattleAbilityTagger>() != null)
        {
            battleImageHolder = GetComponentInChildren<BattleAbilityTagger>().gameObject;
        }
        if(GetComponentInChildren<InventoryAbilityTagger>() != null)
        {
            inventoryImageHolder = GetComponentInChildren<InventoryAbilityTagger>().gameObject;
        }
    }

    //Getters & Setters
    public string GetAbilityName()
    {
        return abilityName;
    }

    public string GetInfo()
    {
        return info;
    }

    public bool GetActivatableStatus()
    {
        return activeAbility;
    }

    public GameObject GetBattleImageHolder()
    {
        return battleImageHolder;
    }

    public GameObject GetInventoryImageHolder()
    {
        return inventoryImageHolder;
    }

    public Sprite GetBattleImageSprite()
    {
        Image[] images = battleImageHolder.GetComponentsInChildren<Image>();

        foreach (Image image in images)
        {
            if (image.gameObject != battleImageHolder.gameObject)
            {
                return image.sprite;
            }
        }
        return null;
    }

    public Sprite GetInventoryImageSprite()
    {
        Image[] images = inventoryImageHolder.GetComponentsInChildren<Image>();

        foreach (Image image in images)
        {
            if (image.gameObject != inventoryImageHolder.gameObject)
            {
                return image.sprite;
            }
        }
        return null;
    }

    public bool GetInactiveStatus()
    {
        return inactive;
    }

    public int GetInventorySlotNr()
    {
        return inventorySlotNr;
    }

    //Abilities
    public virtual void TurnStart()
    {

    }

    public virtual void TurnEnd()
    {

    }

    public virtual void EnemyDeath()
    {

    }

    public virtual int TakeDamage()
    {
        return 0;
    }

    public virtual int Block()
    {
        return 0;
    }

    public virtual void Dodge()
    {

    }
}
