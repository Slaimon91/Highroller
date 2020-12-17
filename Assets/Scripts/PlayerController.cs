using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState { PLAYING, PAUSED };

public class PlayerController : MonoBehaviour
{
    //Config
    public float moveSpeed = 5f;
    [SerializeField] Transform movePoint;
    [SerializeField] Transform inFrontOfPlayerTrigger;
    Vector2 currentDirection;
    GameObject currentInterObj = null;
    [HideInInspector] public bool interacting = false;
    [HideInInspector] public bool tileFlipping = false;
    bool hasFinishedWalking = false;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap colliderTilemap;
    [SerializeField] GroundTile currentTile;
    Direction dir;
    bool tileFlipAxisPressed = false;
    float playerTileOffset = 0.19f;
    Vector3 playerOffsetVector;
    bool waterForm = false;
    int elevation = 0;
    private bool onBridge = false;


    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] LayerMask whatStopsMovementHigh;
    [SerializeField] LayerMask water;
    [SerializeField] LayerMask notFlippable;

    //State
    public GameState state;

    //Cached component references
    Animator animator;
    InFrontOfPlayerTrigger testTrigger;
    [SerializeField] GameObject selectedTilePrefab;
    [SerializeField] GameObject availableTilePrefab;
    private GameObject selectedTile;
    private List<GameObject> availableTiles;
    public PlayerValues playerValues;
    [SerializeField] GameObject infoboxPrefab;
    private TileflipInfobox infoBoxObject;
    private PlayerControlsManager playerControlsManager;
    
    
    [SerializeField] GameObject darkOverlayPrefab;
    private GameObject darkOverlayObject;

    public enum Direction { North, East, South, West, None};

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI gaiaText;
    [SerializeField] TextMeshProUGUI goldenAcornText;
    private GameObject overWorldCanvas;
    private EncounterManager encounterManager;
    public delegate void OnFinishedInteracting();
    public OnFinishedInteracting onFinishedInteractingCallback;
    private Rigidbody2D rb;

    public Vector2 move;
    void Awake()
    {
        state = GameState.PLAYING;
        playerValues.currentOWScene = SceneManager.GetActiveScene().name;
        rb = GetComponent<Rigidbody2D>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }

    void Start()
    {
        
        movePoint.parent = null;
        animator = GetComponent<Animator>();
        playerControlsManager = FindObjectOfType<PlayerControlsManager>();
        availableTiles = new List<GameObject>();
        testTrigger = GetComponentInChildren<InFrontOfPlayerTrigger>();
        inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y - 1 - playerTileOffset);
        dir = GetDirection();
        SetInteractCoordinates(dir);
        currentDirection.y = -1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        overWorldCanvas = FindObjectOfType<OverworldCanvas>().gameObject;
        encounterManager = FindObjectOfType<EncounterManager>();
        playerOffsetVector = new Vector3(0.0f, 0.19f, 0.0f);
    }

    void Update()
    {
        if(state != GameState.PLAYING)
        {
            return;
        }
        healthText.text = playerValues.healthPoints.ToString() + "/" + playerValues.maxHealthPoints.ToString();
        gaiaText.text = playerValues.gaia.ToString() + "/" + playerValues.maxGaia.ToString();
        goldenAcornText.text = playerValues.currency.ToString();
        //Normal state
        if (!interacting && !tileFlipping)
        {
            PlayerMove();
            dir = GetDirection();
            SetInteractCoordinates(dir);
        }
        //If tileflipping
        if (!interacting && tileFlipping)
        {
            TileFlippingUpdate();
        }
    }
    private void FixedUpdate()
    {

    }

    public void Interact()
    {
        if (state != GameState.PLAYING)
        {
            return;
        }
        //Flipping a tile
        if (!interacting && tileFlipping)
        {
            FlipTile();
        }

        //Pressed interact
        if (!tileFlipping)
        {
            GameObject colliding = testTrigger.GetCollidingGameObject();
            if (testTrigger.GetCollidingInteractableStatus())
            {
                IInteractable[] interactable = colliding.GetComponents<IInteractable>();

                foreach(IInteractable obj in interactable)
                {
                    obj.Interact();
                }
            }
        }
    }

    public void SetGameState(GameState gameState)
    {
        state = gameState;

        if(state == GameState.PAUSED)
        {
            overWorldCanvas.SetActive(false);
        }
        else if (state == GameState.PLAYING)
        {
            overWorldCanvas.SetActive(true);
        }
    }

    private void FlipTile() //When you press A after opening the flip grid
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
                    if(colliding.GetComponent<IInteractable>() != null)
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
        if (!(Vector3.Distance(transform.position, movePoint.position) == 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
        else
        {

            if (!IsCollidingInFront(testTrigger.transform.position))
            {
                foreach (GameObject tile in availableTiles)
                {
                    if(tile.transform.position == inFrontOfPlayerTrigger.position)
                    {
                        if(selectedTile != tile)
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
            PlayerTurnInPlace();

            dir = GetDirection();
            SetInteractCoordinates(dir);
        }
    }

    public void ToggleTileFlip() //When you toggle the flip grid
    {
        
        if (state != GameState.PLAYING)
        {
            return;
        }
        //Pressed tileflip
        if (!interacting)
        {
            //Start flipping
            if (!tileFlipping)
            {
                tileFlipping = true;

                StartCoroutine(WaitForFinishWalking());
            }
            //Stop flipping
            else if (tileFlipping && hasFinishedWalking)
            {
                CancelTileflip();
            }
        }
    }

    public void CancelTileflip()
    {
        tileFlipping = false;
        Destroy(selectedTile);
        foreach (GameObject tile in availableTiles)
        {
            Destroy(tile);
        }
        availableTiles.Clear();
        if(infoBoxObject != null)
        {
            Destroy(infoBoxObject.gameObject);
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

    private bool IsCollidingInFront(Vector3 collidingCheckPos)
    {
        if (elevation == 0 && Physics2D.OverlapCircle(collidingCheckPos, .2f, whatStopsMovement) || 
            (elevation == 1 && Physics2D.OverlapCircle(collidingCheckPos, .2f, whatStopsMovementHigh)) ||
            (Physics2D.OverlapCircle(collidingCheckPos, .2f, water) && !waterForm) ||
            (Physics2D.OverlapCircle(collidingCheckPos, .2f, notFlippable)))
        {
            return true;
        }

        return false;
    }

    public void SetInteracting(bool newState)
    {
        interacting = newState;
        if(!newState)   //Interaction has finished
        {
            if (onFinishedInteractingCallback != null)
            {
                onFinishedInteractingCallback?.Invoke();
            }
        }
    }

    void SetInteractCoordinates(Direction dir)
    {
        if (dir == Direction.East)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x + 1, transform.position.y - playerTileOffset);
        }
        if (dir == Direction.West)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x - 1, transform.position.y - playerTileOffset);
        }
        if (dir == Direction.North)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y + 1 - playerTileOffset);
        }
        if (dir == Direction.South)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y - 1 - playerTileOffset);
        }
    }

    public Direction GetDirection()
    {
        if(currentDirection.x > 0) 
        {
            return Direction.East;
        }
        if (currentDirection.x < 0)
        {
            return Direction.West;
        }
        if (currentDirection.y > 0)
        {
            return Direction.North;
        }
        if (currentDirection.y < 0)
        {
            return Direction.South;
        }
        else
        {
            return Direction.None;
        }
    }

    IEnumerator WaitForFinishWalking()
    {
        hasFinishedWalking = false;
        while (!(Vector3.Distance(transform.position, movePoint.position) == 0))
        {
            yield return null;
        }

        dir = GetDirection();
        SetInteractCoordinates(dir);
        NotWalking();
        SpawnAvailableTiles();
        hasFinishedWalking = true;        
    }

    private void SpawnAvailableTiles()
    {
        Vector3 tempEast =  new Vector3(transform.position.x + 1, transform.position.y - playerTileOffset, transform.position.z);
        Vector3 tempWest = new Vector3(transform.position.x - 1, transform.position.y - playerTileOffset, transform.position.z);
        Vector3 tempNorth = new Vector3(transform.position.x, transform.position.y + 1 - playerTileOffset, transform.position.z);
        Vector3 tempSouth = new Vector3(transform.position.x, transform.position.y - 1 - playerTileOffset, transform.position.z);

        SpawnTile(tempEast);
        SpawnTile(tempWest);
        SpawnTile(tempNorth);
        SpawnTile(tempSouth);

        infoBoxObject = Instantiate(infoboxPrefab, overWorldCanvas.transform).GetComponent<TileflipInfobox>();
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
        if(selectedTile != null && infoBoxObject != null)
        {
            infoBoxObject.gameObject.SetActive(true);
            currentTile = groundTilemap.GetTile(new Vector3Int((int)(selectedTile.transform.position.x - 0.5f), (int)(selectedTile.transform.position.y - 0.5f), (int)selectedTile.transform.position.z)) as GroundTile;
            if (currentTile)
            {
                TileflipTable currentTileTable = encounterManager.TestGroundType(currentTile.groundType, selectedTile, true);

                if (currentTileTable != null)
                {
                    string per = "%";
                    if ((playerValues.healthPoints >= playerValues.maxHealthPoints) && (playerValues.gaia >= playerValues.maxGaia))
                    {
                        int monster = currentTileTable.monsterChance + currentTileTable.HPChance + currentTileTable.gaiaChance;
                        infoBoxObject.AssignInfo(currentTileTable.displayName, "0" + per, "0" + per, monster.ToString() + per);
                    }
                    else if (playerValues.healthPoints >= playerValues.maxHealthPoints)
                    {
                        int monster = currentTileTable.monsterChance + currentTileTable.HPChance;
                        infoBoxObject.AssignInfo(currentTileTable.displayName, "0" + per, currentTileTable.gaiaChance.ToString() + per, monster.ToString() + per);
                    }
                    else if (playerValues.gaia >= playerValues.maxGaia)
                    {
                        int monster = currentTileTable.monsterChance + currentTileTable.gaiaChance;
                        infoBoxObject.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, "0" + per, monster.ToString() + per);
                    }
                    else
                    {
                        infoBoxObject.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, currentTileTable.gaiaChance.ToString() + per, currentTileTable.monsterChance.ToString() + per);
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

                        infoBoxObject.AssignInfo(currentTileTable.displayName, currentTileTable.HPChance.ToString() + per, currentTileTable.gaiaChance.ToString() + per, currentTileTable.monsterChance.ToString() + per);
                    }
                }
            }
        }
        else if (infoBoxObject != null)
        {
            infoBoxObject.gameObject.SetActive(false);
        }
    }

    public IEnumerator TeleportPlayer(Vector3 newCoords, Vector2 newDirection)
    {
        NotWalking();
        move.x = 0;
        move.y = 0;
        transform.position = newCoords;
        currentDirection = newDirection;
        movePoint.transform.position = newCoords;
        inFrontOfPlayerTrigger.transform.position = newCoords;
        playerControlsManager.ToggleOnGenericUI();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        playerControlsManager.ToggleOffGenericUI();
    }

    private void PlayerTurnInPlace()
    {
        if (move.x != 0 || move.y != 0)
        {
            currentDirection.x = move.x;
            currentDirection.y = move.y;
        }
        animator.SetFloat("moveX", currentDirection.x);
        animator.SetFloat("moveY", currentDirection.y);
    }

    void NotWalking()
    {
        animator.SetBool("Walking", false);
        animator.SetFloat("moveX", currentDirection.x);
        animator.SetFloat("moveY", currentDirection.y);
    }

    private void PlayerMove()
    {
        float newOffset = 0.3f;
        //Actually move the player closer to the movepoint every frame 
         transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        //Vector3 smoothedDelta = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        // Then apply it to the rigidbody:
        //rb.MovePosition(smoothedDelta);

        if (move.x != 0 || move.y != 0)
        {
            currentDirection.x = move.x;
            currentDirection.y = move.y;
        }

        //Only check movement input if we are at the movepoint position
        if ((Vector3.Distance(transform.position, movePoint.position) <= .05f))
        {
            
            //Check if we're pressing all the way to the left or to the right
            if (move.x == 1f)    //east
            {
                if (elevation == 0 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x - newOffset, 0f, 0f), .2f, whatStopsMovement) || (elevation == 1 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x - newOffset, 0f, 0f), .2f, whatStopsMovementHigh)))
                {
                    if((!Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x - newOffset, 0f, 0f), .2f, water) || waterForm || onBridge))
                    {
                        movePoint.position += new Vector3(move.x, 0f, 0f);
                        animator.SetFloat("moveX", currentDirection.x);
                        animator.SetFloat("moveY", 0);
                    }
                    else
                    {
                        NotWalking();
                    }
                }
                else
                {
                    NotWalking();
                }
            }

            //Check if we're pressing all the way to the left or to the right
            else if (move.x == -1f)   //west
            {
                if (elevation == 0 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x + newOffset, 0f, 0f), .2f, whatStopsMovement) || (elevation == 1 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x + newOffset, 0f, 0f), .2f, whatStopsMovementHigh)))
                {
                    if ((!Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x + newOffset, 0f, 0f), .2f, water) || waterForm || onBridge))
                    {
                        movePoint.position += new Vector3(move.x, 0f, 0f);
                        animator.SetFloat("moveX", currentDirection.x);
                        animator.SetFloat("moveY", 0);
                    }
                    else
                    {
                        NotWalking();
                    }
                }
                else
                {
                    NotWalking();
                }
            }

            else if (move.y == 1f)   //north
            {

                if (elevation == 0 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y - newOffset - playerTileOffset, 0f), .2f, whatStopsMovement) || (elevation == 1 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y - newOffset - playerTileOffset, 0f), .2f, whatStopsMovementHigh)))
                {
                    if ((!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y - newOffset - playerTileOffset, 0f), .2f, water) || waterForm || onBridge))
                    {
                        movePoint.position += new Vector3(0f, move.y, 0f);
                        animator.SetFloat("moveY", currentDirection.y);
                        animator.SetFloat("moveX", 0);
                    }
                    else
                    {
                        NotWalking();
                    }
                }
                else
                {
                    NotWalking();

                }
            }

            else if (move.y == -1f)   //south
            {

                if (elevation == 0 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y + newOffset + playerTileOffset, 0f), .2f, whatStopsMovement) || (elevation == 1 && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y + newOffset + playerTileOffset, 0f), .2f, whatStopsMovementHigh)))
                {
                    if ((!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y + newOffset + playerTileOffset, 0f), .2f, water) || waterForm || onBridge))
                    {
                        movePoint.position += new Vector3(0f, move.y, 0f);
                        animator.SetFloat("moveY", currentDirection.y);
                        animator.SetFloat("moveX", 0);
                    }
                    else
                    {
                        NotWalking();
                    }
                }
                else
                {
                    NotWalking();

                }
            }

            else
            {
                NotWalking();
            }
        }
        else
        {
            animator.SetBool("Walking", true);
        }
    }

    public Vector2 GetCurrentDirection()
    {
        return currentDirection;
    }

    public void ToggleWaterTransformation()
    {
        if(waterForm)
        {
            waterForm = false;
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
            sprite.sortingLayerName = "Player";
            sprite.sortingOrder = 0;
        }
        else
        {
            waterForm = true;
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.color = new Color(0f / 255f, 210f / 255f, 255f / 255f);
            sprite.sortingLayerName = "Ground";
            sprite.sortingOrder = 1;
        }
    }

    public void ElevationChangePlayer(int fromLevel, int toLevel)
    {
        if(elevation == fromLevel)
        {
            elevation = toLevel;
        }

        if(elevation == 0)
        {
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sortingLayerName = "Player";
            sprite.sortingOrder = 0;
            movePoint.gameObject.layer = 0;
        }
        else if (elevation == 1)
        {
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sortingLayerName = "All tiles Overlay";
            sprite.sortingOrder = 1;
            movePoint.gameObject.layer = 9;
        }
    }

    public void SetOnBridge(bool status)
    {
        onBridge = status;
    }

    private void Save(string temp)
    {
        SaveData.current.playerOW = new PlayerData(gameObject.GetComponent<PlayerController>());
        //SaveSystem.Save<PlayerData>(new PlayerData(gameObject.GetComponent<PlayerController>()),"", playerValues.currentSavefile + "/" + temp + playerValues.currentOWScene + "/PlayerData");
        SaveSystem.Save<SavefileDisplayData>(new SavefileDisplayData(playerValues), playerValues.currentSavefile + "/" + temp + "SavefileDisplay");
    }

    public void Load(string temp)
    {
        PlayerData data = SaveData.current.playerOW;//SaveSystem.Load<PlayerData>("", playerValues.currentSavefile + "/" + temp + playerValues.currentOWScene +  "/PlayerData");
        
        if(data != default)
        {
            if(temp == "")
            {
                playerValues.healthPoints = data.healthPoints;
                playerValues.maxHealthPoints = data.maxHealthPoints;
                playerValues.gaia = data.gaia;
                playerValues.maxGaia = data.maxGaia;
                playerValues.currency = data.currency;
                playerValues.xp = data.xp;
                playerValues.level = data.level;
                playerValues.nrOfBattles = data.nrOfBattles;
            }

            transform.position = data.position;
            currentDirection = data.direction;
            movePoint.transform.position = transform.position;
        }
        else
        {
            playerValues.healthPoints = 20;
            playerValues.maxHealthPoints = 20;
            playerValues.gaia = 0;
            playerValues.maxGaia = 50;
            playerValues.currency = 0;
            playerValues.xp = 0;
            playerValues.level = 1;
            playerValues.nrOfBattles = 0;
        }
    }
    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }

    public void LoadPlayerAtCoords(Vector3 newPosition, Vector2 newDirection)
    {
        transform.position = newPosition;
        currentDirection = newDirection;
        movePoint.transform.position = transform.position;
    }
}

[System.Serializable]
public class PlayerData
{
    public int healthPoints;
    public int maxHealthPoints;
    public int gaia;
    public int maxGaia;
    public int currency;
    public int xp;
    public int level;
    public int nrOfBattles;
    public string currentOWScene;
    public int currentSavefile;

    public Vector3 position;
    public Vector2 direction;

    public PlayerData(PlayerController playerController)
    {
        healthPoints = playerController.playerValues.healthPoints;
        maxHealthPoints = playerController.playerValues.maxHealthPoints;
        gaia = playerController.playerValues.gaia;
        maxGaia = playerController.playerValues.maxGaia;
        currency = playerController.playerValues.currency;
        xp = playerController.playerValues.xp;
        level = playerController.playerValues.level;
        nrOfBattles = playerController.playerValues.nrOfBattles;
        currentOWScene = playerController.playerValues.currentOWScene;
        currentSavefile = playerController.playerValues.currentSavefile;

        position = playerController.transform.position;
        direction = playerController.GetCurrentDirection();
    }
}