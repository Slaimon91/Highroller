using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTile : MonoBehaviour
{
    [SerializeField] Sprite battleBackground;
    [SerializeField] Encounter encounter;

    public Sprite GetBattleBackground()
    {
        return battleBackground;
    }
    public Encounter GetEncounter()
    {
        return encounter;
    }
}
