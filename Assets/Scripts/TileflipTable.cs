using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Table", menuName = "Highroller/TileflipTable")]
public class TileflipTable : ScriptableObject
{
    public new string name = "New Item";
    public Sprite battleBackground;
    public string displayName;
    public int gaiaChance;
    public int HPChance;
    public int monsterChance;
    public int gaiaRewardAmount;
    public int HPRewardAmount;
    public List<Sprite> monsterIcons;
    public List<Encounter> encounters;

    public virtual void Use()
    {
        //Use item

        Debug.Log("Using" + name);
    }
}

[System.Serializable]
public class Encounter
{
    public List<GameObject> list;
    public int weight;
    public Encounter()
    {
        list = new List<GameObject>();
    }
}
