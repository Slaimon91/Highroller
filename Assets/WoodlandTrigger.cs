using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WoodlandTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToActivate = new List<GameObject>();
    [SerializeField] List<GameObject> objectsToDeactivate = new List<GameObject>();
    [SerializeField] bool isEntrance;

    [SerializeField] Tile grassTile;
    [SerializeField] Tile caveOpening;
    [SerializeField] Tilemap inFrontMap;
    [SerializeField] Vector3Int firstCell;
    [SerializeField] Vector3Int secondCell;
    [SerializeField] Vector3Int thirdCell;
    [SerializeField] Vector3Int fourthCell;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (isEntrance)
            {
                foreach (GameObject obj in objectsToActivate)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in objectsToDeactivate)
                {
                    obj.SetActive(false);
                }

                ToggleInfrontTiles(true);
            }
            else
            {
                foreach (GameObject obj in objectsToActivate)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in objectsToDeactivate)
                {
                    obj.SetActive(true);
                }

                ToggleInfrontTiles(false);
            }
        }
    }

    private void ToggleInfrontTiles(bool status)
    {
        if(status)
        {
            inFrontMap.SetTile(firstCell, grassTile);
            inFrontMap.SetTile(secondCell, grassTile);
            inFrontMap.SetTile(thirdCell, grassTile);
            inFrontMap.SetTile(fourthCell, caveOpening);
        }
        else
        {
            inFrontMap.SetTile(firstCell, null);
            inFrontMap.SetTile(secondCell, null);
            inFrontMap.SetTile(thirdCell, null);
            inFrontMap.SetTile(fourthCell, null);
        }
    }
}
