using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    [SerializeField]
    [TextArea(3, 20)]
    public string info;

    [SerializeField] GameObject battleImageHolder;

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
