using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public enum GameState { PLAYING, PAUSED };

public class PlayerController : MonoBehaviour
{
    //Config
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform movePoint;
    [SerializeField] Transform inFrontOfPlayerTrigger;
    Vector2 currentDirection;
    GameObject currentInterObj = null;
    bool interacting = false;
    bool tileFlipping = false;
    bool hasFinishedWalking = false;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap colliderTilemap;
    [SerializeField] GroundTile currentTile;
    Direction dir;
    bool tileFlipAxisPressed = false;
    int pendingGaiaReward = 0;
    int pendingHPReward = 0;
    int pendingXPReward = 0;


    [SerializeField] LayerMask whatStopsMovement;

    //State
    public GameState state;

    //Cached component references
    Animator animator;
    InFrontOfPlayerTrigger testTrigger;
    [SerializeField] GameObject selectedTilePrefab;
    [SerializeField] GameObject availableTilePrefab;
    private GameObject selectedTile;
    private List<GameObject> availableTiles;
    [SerializeField] PlayerValues playerValues;
    [SerializeField] GameObject rewardbox;
    [SerializeField] GameObject battleRewardbox;
    [SerializeField] Sprite gaiaSprite;
    [SerializeField] Sprite HPSprite;
    [SerializeField] Sprite XPSprite;

    enum Direction { North, East, South, West, None};

    [SerializeField] TextMeshProUGUI healthText;
    private GameObject healthTextGameObject;
    [SerializeField] TextMeshProUGUI gaiaText;
    private GameObject gaiaTextGameObject;
    private GameObject overWorldCanvas;
    private EncounterManager encounterManager;

    public Vector2 move;
    void Awake()
    {
        state = GameState.PLAYING;
    }

    void Start()
    {
        movePoint.parent = null;
        animator = GetComponent<Animator>();
        availableTiles = new List<GameObject>();
        testTrigger = GetComponentInChildren<InFrontOfPlayerTrigger>();
        inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y - 1);
        dir = GetDirection();
        SetInteractCoordinates(dir);
        currentDirection.y = -1f;
        healthTextGameObject = GameObject.FindGameObjectWithTag("HealthText");
        healthText = healthTextGameObject.GetComponent<TextMeshProUGUI>();
        gaiaTextGameObject = GameObject.FindGameObjectWithTag("GaiaText");
        gaiaText = gaiaTextGameObject.GetComponent<TextMeshProUGUI>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        overWorldCanvas = FindObjectOfType<OverworldCanvas>().gameObject;
        encounterManager = FindObjectOfType<EncounterManager>();
    }

    void Update()
    {
        if(state != GameState.PLAYING)
        {
            return;
        }
        healthText.text = playerValues.healthPoints.ToString() + "/" + playerValues.maxHealthPoints.ToString();
        gaiaText.text = playerValues.gaia.ToString() + "/" + playerValues.maxGaia.ToString();
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
                colliding.GetComponent<IInteractable>().Interact();
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

    private void FlipTile()
    {
        if (selectedTile)
        {
            currentTile = groundTilemap.GetTile(new Vector3Int((int)(selectedTile.transform.position.x - 0.5f), (int)(selectedTile.transform.position.y - 0.5f), (int)selectedTile.transform.position.z)) as GroundTile;
            if (currentTile)
            {
                TestGroundType(currentTile.groundType);

            }
            else
            {
                GameObject colliding = testTrigger.GetCollidingGameObject();
                if (testTrigger.GetCollidingTileableStatus())
                {
                    GroundType tmpType = colliding.GetComponent<TTileable>().GetTileType();

                    TestGroundType(tmpType);
                }
            }
        }
    }

    private void TestGroundType(GroundType groundTile)
    {
        if (groundTile == GroundType.GreenforestGrass)
        {
            encounterManager.GreenforestGrass();
        }
        if (groundTile == GroundType.GreenforestSwamp)
        {
            encounterManager.GreenforestSwamp();
        }
        if (groundTile == GroundType.GreenforestWater)
        {
            encounterManager.GreenforestWater();
        }
    }

    private void TileFlippingUpdate()
    {
        if (!(Vector3.Distance(transform.position, movePoint.position) == 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
        else
        {

            if (IsCollidingNotInFront())
            {
                foreach (GameObject tile in availableTiles)
                {
                    if(tile.transform.position == inFrontOfPlayerTrigger.position)
                    {
                        selectedTile = tile;
                        tile.GetComponent<Animator>().SetFloat("Available", 1f);
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

            PlayerTurnInPlace();

            dir = GetDirection();
            SetInteractCoordinates(dir);
        }
    }

    public void PressedTileFlip()
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

    private void CancelTileflip()
    {
        tileFlipping = false;
        Destroy(selectedTile);
        foreach (GameObject tile in availableTiles)
        {
            Destroy(tile);
        }
        availableTiles.Clear();
    }

    private bool IsCollidingNotInFront()
    {
        if (!Physics2D.OverlapCircle(testTrigger.transform.position, .2f, whatStopsMovement))
        {
            return true;
        }

        return false;
    }

    public void SetInteracting(bool newState)
    {
        interacting = newState;
    }

    void SetInteractCoordinates(Direction dir)
    {
        if (dir == Direction.East)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (dir == Direction.West)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
        if (dir == Direction.North)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
        if (dir == Direction.South)
        {
            inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y - 1);
        }
    }

    Direction GetDirection()
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
        Vector3 tempEast =  new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        Vector3 tempWest = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        Vector3 tempNorth = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 tempSouth = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

        //East
        if (!Physics2D.OverlapCircle(tempEast, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempEast, Quaternion.identity));
            availableTile.transform.parent = transform;
            availableTiles.Add(availableTile);
            if (availableTile.transform.position == inFrontOfPlayerTrigger.position)
            {
                selectedTile = availableTile;
                availableTile.GetComponent<Animator>().SetFloat("Available", 1f);
            }
        }

        //West
        if (!Physics2D.OverlapCircle(tempWest, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempWest, Quaternion.identity));
            availableTile.transform.parent = transform;
            availableTiles.Add(availableTile);
            if (availableTile.transform.position == inFrontOfPlayerTrigger.position)
            {
                selectedTile = availableTile;
                availableTile.GetComponent<Animator>().SetFloat("Available", 1f);
            }
        }

        //North
        if (!Physics2D.OverlapCircle(tempNorth, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempNorth, Quaternion.identity));
            availableTile.transform.parent = transform;
            availableTiles.Add(availableTile);
            if (availableTile.transform.position == inFrontOfPlayerTrigger.position)
            {
                selectedTile = availableTile;
                availableTile.GetComponent<Animator>().SetFloat("Available", 1f);
            }
        }

        //South
        if (!Physics2D.OverlapCircle(tempSouth, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempSouth, Quaternion.identity));
            availableTile.transform.parent = transform;
            availableTiles.Add(availableTile);
            if (availableTile.transform.position == inFrontOfPlayerTrigger.position)
            {
                selectedTile = availableTile;
                availableTile.GetComponent<Animator>().SetFloat("Available", 1f);
            }
        }
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

        //Actually move the player closer to the movepoint every frame 
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (move.x != 0 || move.y != 0)
        {
            currentDirection.x = move.x;
            currentDirection.y = move.y;
        }

        //Only check movement input if we are at the movepoint position
        if ((Vector3.Distance(transform.position, movePoint.position) <= .05f))
        {

            //Check if we're pressing all the way to the left or to the right
            if (Mathf.Abs(move.x) == 1f)
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(move.x, 0f, 0f), .2f, whatStopsMovement))
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

            else if (Mathf.Abs(move.y) == 1f)
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, move.y, 0f), .2f, whatStopsMovement))
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
            animator.SetBool("Walking", true);
        }
    }

    public void LanuchGaiaRewardbox(int amount)
    {
        string gaiaText = amount + " Gaia";
        GameObject popup = Instantiate(rewardbox, overWorldCanvas.transform);
        popup.GetComponent<TileflipRewardbox>().AssignInfo(gaiaText, gaiaSprite);
        popup.GetComponent<TileflipRewardbox>().onAcceptRewardCallback += AcceptReward;
        pendingGaiaReward = amount;
        CancelTileflip();
    }

    public void LanuchHPRewardbox(int amount)
    {
        string HPText = amount + " HP";
        GameObject popup = Instantiate(rewardbox, overWorldCanvas.transform);
        popup.GetComponent<TileflipRewardbox>().AssignInfo(HPText, HPSprite);
        popup.GetComponent<TileflipRewardbox>().onAcceptRewardCallback += AcceptReward;
        pendingHPReward = amount;
        CancelTileflip();
    }

    public void LanuchBattleRewardbox(int amount, int multiplier)
    {
        string XPText;
        if (multiplier > 1)
        {
            XPText = amount + "XP";
        }
        else
        {
            XPText = amount + "XP";
        }
        
        GameObject popup = Instantiate(battleRewardbox, overWorldCanvas.transform);
        popup.GetComponent<TileflipRewardbox>().AssignInfo(XPText, XPSprite);
        popup.GetComponent<TileflipRewardbox>().onAcceptRewardCallback += AcceptReward;
        StartCoroutine(ShowXP(popup, amount, multiplier));
        pendingHPReward = amount*multiplier;
    }

    IEnumerator ShowXP(GameObject popup, int amount, int multiplier)
    {
        string XPText;
        yield return new WaitForSeconds(2);
        XPText = amount + "XP x" + multiplier;
        popup.GetComponent<TileflipRewardbox>().AssignInfo(XPText, XPSprite);
        yield return new WaitForSeconds(2);
        XPText = amount * multiplier + "XP";
        popup.GetComponent<TileflipRewardbox>().AssignInfo(XPText, XPSprite);


    }

    public void AcceptReward()
    {
        if(pendingGaiaReward > 0)
        {
            playerValues.gaia += pendingGaiaReward;
            pendingGaiaReward = 0;
        }

        if (pendingHPReward > 0)
        {
            playerValues.healthPoints += pendingHPReward;
            pendingHPReward = 0;
        }

        if (pendingXPReward > 0)
        {
            playerValues.xp += pendingXPReward;
            pendingXPReward = 0;
        }
    }
}
