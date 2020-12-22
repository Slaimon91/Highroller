using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileflipManager : MonoBehaviour
{

    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap colliderTilemap;

    [SerializeField] GameObject selectedTilePrefab;
    [SerializeField] GameObject availableTilePrefab;

    [SerializeField] Transform movePoint;
    [SerializeField] Transform inFrontOfPlayerTrigger;
    [SerializeField] GameObject infoboxPrefab;
    [SerializeField] GameObject infoboxPrefabMonster;
    [SerializeField] Sprite unknownMonster;
    private TileflipInfobox infoBoxObject;
    private TileflipInfoboxMonster infoBoxObjectMonster;
    private GameObject selectedTile;
    private List<GameObject> availableTiles;
    private PlayerControlsManager playerControlsManager;
    private GroundTile currentTile;
    private EncounterManager encounterManager;
    private PlayerController playerController;
    [SerializeField] PlayerValues playerValues;
    private InFrontOfPlayerTrigger testTrigger;
    private GameObject overWorldCanvas;

    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] LayerMask whatStopsMovementHigh;
    [SerializeField] LayerMask water;
    [SerializeField] LayerMask notFlippable;


    private void Start()
    {
        availableTiles = new List<GameObject>();
        playerControlsManager = FindObjectOfType<PlayerControlsManager>();
        encounterManager = FindObjectOfType<EncounterManager>();
        overWorldCanvas = FindObjectOfType<OverworldCanvas>().gameObject;
        testTrigger = FindObjectOfType<InFrontOfPlayerTrigger>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void LateUpdate()
    {
        //If tileflipping
        if (!playerController.interacting && playerController.tileFlipping)
        {
            TileFlippingUpdate();
        }
    }

    public void FlipTile() //When you press A after opening the flip grid
    {
        if (selectedTile)
        {
            playerControlsManager.ToggleOnGenericUI();
            currentTile = groundTilemap.GetTile(new Vector3Int((int)(selectedTile.transform.position.x - 0.5f), (int)(selectedTile.transform.position.y - 0.5f), (int)selectedTile.transform.position.z)) as GroundTile;
            if (currentTile)
            {
                //darkOverlayObject = Instantiate(darkOverlayPrefab, transform);
                encounterManager.TestGroundType(currentTile.groundType, selectedTile, false);
            }
            else
            {
                GameObject colliding = testTrigger.GetCollidingGameObject();
                if (testTrigger.GetCollidingTileableStatus())
                {
                    if (colliding.GetComponent<IInteractable>() != null)
                    {
                        colliding.GetComponent<IInteractable>().Interact();
                    }
                    GroundType tmpType = colliding.GetComponent<TTileable>().GetTileType();
                    //darkOverlayObject = Instantiate(darkOverlayPrefab, transform);
                    encounterManager.TestGroundType(tmpType, selectedTile, false);
                }
            }
        }
    }

    private void TileFlippingUpdate() //Runs every frame while the grid is open
    {
        if (!(Vector3.Distance(playerController.transform.position, movePoint.position) == 0))
        {
            playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, movePoint.position, playerController.moveSpeed * Time.deltaTime);
        }
        else
        {

            if (!IsCollidingInFront(testTrigger.transform.position))
            {
                foreach (GameObject tile in availableTiles)
                {
                    if (tile.transform.position == inFrontOfPlayerTrigger.position)
                    {
                        if (selectedTile != tile)
                        {
                            selectedTile = tile;
                            tile.GetComponent<Animator>().SetFloat("Available", 1f);
                        }
                    }
                    else
                    {
                        tile.GetComponent<Animator>().SetFloat("Available", -1f);
                    }
                }
            }
            else
            {
                selectedTile = null;
                foreach (GameObject tile in availableTiles)
                {
                    var TileAnimator = tile.GetComponent<Animator>();
                    TileAnimator.SetFloat("Available", -1f);
                }
            }

            UpdateInfobox();
        }
    }

    private bool IsCollidingInFront(Vector3 collidingCheckPos)
    {
        if (playerController.elevation == 0 && Physics2D.OverlapCircle(collidingCheckPos, .2f, whatStopsMovement) ||
            (playerController.elevation == 1 && Physics2D.OverlapCircle(collidingCheckPos, .2f, whatStopsMovementHigh)) ||
            (Physics2D.OverlapCircle(collidingCheckPos, .2f, water) && !playerController.waterForm) ||
            (Physics2D.OverlapCircle(collidingCheckPos, .2f, notFlippable)))
        {
            return true;
        }

        return false;
    }



    public void CancelTileflip()
    {

        Destroy(selectedTile);
        foreach (GameObject tile in availableTiles)
        {
            Destroy(tile);
        }
        availableTiles.Clear();
        if (infoBoxObject != null)
        {
            Destroy(infoBoxObject.gameObject);
        }
        if (infoBoxObjectMonster != null)
        {
            Destroy(infoBoxObjectMonster.gameObject);
        }
    }

    public void RemoveFlipSquares()
    {
        foreach (GameObject tile in availableTiles)
        {
            if (tile != selectedTile)
                tile.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        if (infoBoxObject != null)
        {
            Destroy(infoBoxObject.gameObject);
        }
    }

    public void SpawnAvailableTiles()
    {
        Vector3 tempEast = new Vector3(playerController.transform.position.x + 1, playerController.transform.position.y - playerController.playerTileOffset, 0);
        Vector3 tempWest = new Vector3(playerController.transform.position.x - 1, playerController.transform.position.y - playerController.playerTileOffset, 0);
        Vector3 tempNorth = new Vector3(playerController.transform.position.x, playerController.transform.position.y + 1 - playerController.playerTileOffset, 0);
        Vector3 tempSouth = new Vector3(playerController.transform.position.x, playerController.transform.position.y - 1 - playerController.playerTileOffset, 0);

        SpawnTile(tempEast);
        SpawnTile(tempWest);
        SpawnTile(tempNorth);
        SpawnTile(tempSouth);

        infoBoxObject = Instantiate(infoboxPrefab, overWorldCanvas.transform).GetComponent<TileflipInfobox>();
        infoBoxObjectMonster = Instantiate(infoboxPrefabMonster, overWorldCanvas.transform).GetComponent<TileflipInfoboxMonster>();
        UpdateInfobox();
    }

    private void SpawnTile(Vector3 direction)
    {
        if (!IsCollidingInFront(direction))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, direction, Quaternion.identity));
            availableTile.transform.parent = transform;
            availableTiles.Add(availableTile);
            if (availableTile.transform.position == inFrontOfPlayerTrigger.position)
            {
                selectedTile = availableTile;
                availableTile.GetComponent<Animator>().SetFloat("Available", 1f);
            }
        }
    }

    private void UpdateInfobox()
    {
        if (selectedTile != null && (infoBoxObject != null || infoBoxObjectMonster != null))
        {
            currentTile = groundTilemap.GetTile(new Vector3Int((int)(selectedTile.transform.position.x - 0.5f), (int)(selectedTile.transform.position.y - 0.5f), (int)selectedTile.transform.position.z)) as GroundTile;
            if (currentTile)
            {
                TileflipTable currentTileTable = encounterManager.TestGroundType(currentTile.groundType, selectedTile, true);

                if (currentTileTable != null)
                {
                    string per = "%";
                    if(infoBoxObject != null)
                        infoBoxObject.gameObject.SetActive(false);
                    if(infoBoxObjectMonster != null)
                        infoBoxObjectMonster.gameObject.SetActive(true);
                    if ((playerValues.healthPoints >= playerValues.maxHealthPoints) && (playerValues.gaia >= playerValues.maxGaia))
                    {
                        int monster = currentTileTable.monsterChance + currentTileTable.HPChance + currentTileTable.gaiaChance;
                        infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, "MAX", "MAX", monster.ToString() + per, currentTileTable.monsterIcons);
                    }
                    else if (playerValues.healthPoints >= playerValues.maxHealthPoints)
                    {
                        int monster = currentTileTable.monsterChance + currentTileTable.HPChance;
                        infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, "MAX", currentTileTable.gaiaChance.ToString() + per, monster.ToString() + per, currentTileTable.monsterIcons);
                    }
                    else if (playerValues.gaia >= playerValues.maxGaia)
                    {
                        int monster = currentTileTable.monsterChance + currentTileTable.gaiaChance;
                        infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, "MAX", monster.ToString() + per, currentTileTable.monsterIcons);
                    }
                    else
                    {
                        infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, currentTileTable.gaiaChance.ToString() + per, currentTileTable.monsterChance.ToString() + per, currentTileTable.monsterIcons);
                    }
                }
            }
            else
            {
                GameObject colliding = testTrigger.GetCollidingGameObject();
                if (testTrigger.GetCollidingTileableStatus())
                {
                    GroundType tmpType = colliding.GetComponent<TTileable>().GetTileType();
                    TileflipTable currentTileTable = encounterManager.TestGroundType(tmpType, selectedTile, true);

                    if (currentTileTable != null)
                    {
                        string per = "%";
                        if(currentTileTable.monsterChance == 0)
                        {
                            if (infoBoxObject != null)
                                infoBoxObject.gameObject.SetActive(true);
                            if (infoBoxObjectMonster != null)
                                infoBoxObjectMonster.gameObject.SetActive(false);
                            infoBoxObject.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, currentTileTable.gaiaChance.ToString() + per, currentTileTable.monsterChance.ToString() + per);
                        }
                        else
                        {
                            if (infoBoxObject != null)
                                infoBoxObject.gameObject.SetActive(false);
                            if (infoBoxObjectMonster != null)
                                infoBoxObjectMonster.gameObject.SetActive(true);
                            if ((playerValues.healthPoints >= playerValues.maxHealthPoints) && (playerValues.gaia >= playerValues.maxGaia))
                            {
                                int monster = currentTileTable.monsterChance + currentTileTable.HPChance + currentTileTable.gaiaChance;
                                infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, "MAX", "MAX", monster.ToString() + per, currentTileTable.monsterIcons);
                            }
                            else if (playerValues.healthPoints >= playerValues.maxHealthPoints)
                            {
                                int monster = currentTileTable.monsterChance + currentTileTable.HPChance;
                                infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, "MAX", currentTileTable.gaiaChance.ToString() + per, monster.ToString() + per, currentTileTable.monsterIcons);
                            }
                            else if (playerValues.gaia >= playerValues.maxGaia)
                            {
                                int monster = currentTileTable.monsterChance + currentTileTable.gaiaChance;
                                infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, "MAX", monster.ToString() + per, currentTileTable.monsterIcons);
                            }
                            else
                            {
                                infoBoxObjectMonster.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, currentTileTable.gaiaChance.ToString() + per, currentTileTable.monsterChance.ToString() + per, currentTileTable.monsterIcons);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (infoBoxObject != null)
                infoBoxObject.gameObject.SetActive(false);
            if (infoBoxObjectMonster != null)
                infoBoxObjectMonster.gameObject.SetActive(false);
        }
    }
}
