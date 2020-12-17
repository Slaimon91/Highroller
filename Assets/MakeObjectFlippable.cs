using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MakeObjectFlippable : MonoBehaviour, TTileable
{
    [SerializeField] GroundType groundType;
    public GroundType GetTileType()
    {
        return groundType;
    }
}
