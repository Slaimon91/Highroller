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

    [SerializeField] protected GameObject battleImageHolder;

    [SerializeField] protected bool activeAbility = false;

    protected bool inactive = false;

    void Awake()
    {
        transform.SetSiblingIndex(0);
        battleImageHolder = GetComponentInChildren<Image>().gameObject;
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

    public bool GetInactiveStatus()
    {
        return inactive;
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
