using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lexikon", menuName = "Highroller/EnemyLexikon")]
public class EnemyLexikon : ScriptableObject
{
    public new string name = "New Lexikon";
    public List<EnemyEntry> entries;
}

[System.Serializable]
public class EnemyEntry
{
    public int enemyNumber;
    public string enemyName;
    public bool hasEncountered;
    public int soulCount;

    [TextArea(3, 20)]
    public string info;
    public Sprite enemyIcon;
}
