using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Table", menuName = "Highroller/BattleStartupInfo")]
public class BattleStartupInfo : ScriptableObject
{
    public new string name = "New Item";
    public Sprite battleBackground;
    public List<GameObject> enemies;
    public int nrOfDices;

    public virtual void Use()
    {
        //Use item

        Debug.Log("Using" + name);
    }
}