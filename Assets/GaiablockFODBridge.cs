using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GaiablockFODBridge : Gaiablockade
{
    [SerializeField] Tile leftBackBridgeTile;
    [SerializeField] Tile leftFrontBridgeTile;
    [SerializeField] Tile rightBackBridgeTile;
    [SerializeField] Tile rightFrontBridgeTile;
    [SerializeField] Tilemap colliderMap;
    [SerializeField] Tilemap colliderInFrontMap;
    [SerializeField] Vector3Int leftCell;
    [SerializeField] Vector3Int rightCell;
    public override void PaidGaiaBlock()
    {
        /*colliderMap.SetTile(leftCell, leftBackBridgeTile);
        colliderMap.SetTile(rightCell, rightBackBridgeTile);
        colliderInFrontMap.SetTile(leftCell, leftFrontBridgeTile);
        colliderInFrontMap.SetTile(rightCell, rightFrontBridgeTile);*/
    }
}
