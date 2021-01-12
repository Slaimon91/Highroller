using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableObjectGenerator : MonoBehaviour
{
    [SerializeField] GameObject interactBox;
    [SerializeField] List<Tilemap> tilemapsToCheck = new List<Tilemap>();
    [TextArea(3, 20)] public List<string> genericText = new List<string>();

    private void Start()
    {
        foreach(Tilemap tilemap in tilemapsToCheck)
        {
            GenerateInteractables(tilemap);
        }
    }

    private void GenerateInteractables(Tilemap tileMap)
    {
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //InteractableInfoTile tile = tileMap.GetTile(new Vector3Int((int)(place.x), (int)(place.y), (int)(place.z))) as InteractableInfoTile;
                    //Debug.Log(tile + "localplace: " + localPlace + "place: " + place);
                    TileBase tile = tileMap.GetTile(localPlace);
                    string boxToSpawn = CheckTile(tile);
                    if (boxToSpawn != null)
                    {
                        var newObject = Instantiate(interactBox, place, gameObject.transform.rotation);
                        newObject.transform.parent = gameObject.transform;
                        newObject.GetComponent<GenericTextTrigger>().SetText(boxToSpawn);
                    }
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }

    private string CheckTile(TileBase tile)
    {
        if (tile.name == "Tilemap_FoD_32") //bush
        {
            return genericText[0];
        }
        else if (tile.name == "Tilemap_FoD_33" || tile.name == "Tilemap_FoD_34") // well
        {
            return genericText[1];
        }
        else if (tile.name == "Tilemap_FoD_35" || tile.name == "Tilemap_FoD_36") // wide tree
        {
            return genericText[2];
        }
        else if (tile.name == "Tilemap_FoD_37") // tall tree
        {
            return genericText[3];
        }
        else if (tile.name == "Tilemap_FoD_139") // short tree
        {
            return genericText[4];
        }
        else if (tile.name == "Tilemap_FoD_3") // sten
        {
            return genericText[5];
        }
        else if (tile.name == "Tilemap_FoD_4") // stump
        {
            return genericText[6];
        }
        else if (tile.name == "Tilemap_FoD_5" || tile.name == "Tilemap_FoD_6") // hollow stump
        {
            return genericText[7];
        }
        else if (tile.name == "Tilemap_FoD_42" || tile.name == "Tilemap_FoD_152") // closed cave
        {
            return genericText[8];
        }
        else if (tile.name == "Tilemap_FoD_27" || tile.name == "Tilemap_FoD_126") // döskalle
        {
            return genericText[9];
        }
        else if (tile.name == "Tilemap_FoD_15" || tile.name == "Tilemap_FoD_125") // fossil
        {
            return genericText[10];
        }
        else if (tile.name == "Tilemap_FoD_14") // wall flower
        {
            return genericText[11];
        }

        return null;
    }
}
