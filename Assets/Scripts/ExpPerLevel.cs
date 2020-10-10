using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ExpPerLevel", menuName = "Highroller/ExpPerLevel")]
public class ExpPerLevel : ScriptableObject
{
    public new string name = "New Item";
    public List<PerLevelExp> expPerLevelTable;
}

[System.Serializable]
public class PerLevelExp
{
    public int level;
    public int exp;
    public int totalExp;
}