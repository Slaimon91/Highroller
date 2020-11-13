using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Values", menuName = "Highroller/PlayerValues")]
public class PlayerValues : ScriptableObject
{
    public new string name = "New Values";
    public int healthPoints;
    public int maxHealthPoints;
    public int gaia;
    public int maxGaia;
    public int seedStage;
    public int currency;
    public int xp;
    public int level;
    public int nrOfBattles;

    public virtual void Use()
    {
        //Use item

        Debug.Log("Using" + name);
    }
}