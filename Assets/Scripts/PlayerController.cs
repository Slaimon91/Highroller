﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //Config
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform movePoint;
    [SerializeField] Transform inFrontOfPlayerTrigger;
    Vector2 currentDirection;
    [SerializeField] GameObject currentInterObj = null;
    bool interacting = false;
    bool tileFlipping = false;
    bool hasFinishedWalking = false;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap colliderTilemap;
    [SerializeField] TileBase currentTile;
    Direction dir;
    KeyCode key = KeyCode.None;

    [SerializeField] LayerMask whatStopsMovement;

    //State

    //Cached component references
    Animator animator;
    [SerializeField] InFrontOfPlayerTrigger testTrigger;
    [SerializeField] GameObject selectedTilePrefab;
    [SerializeField] GameObject availableTilePrefab;
    private GameObject selectedTile;
    private List<GameObject> availableTiles;

    enum Direction { North, East, South, West, None};

    void Start()
    {
        movePoint.parent = null;
        animator = GetComponent<Animator>();
        availableTiles = new List<GameObject>();
        testTrigger = GetComponentInChildren<InFrontOfPlayerTrigger>();
        inFrontOfPlayerTrigger.position = new Vector2(transform.position.x, transform.position.y - 1);
        dir = GetDirection();
        SetInteractCoordinates(dir);
    }

    void Update()
    {
        currentTile = colliderTilemap.GetTile(new Vector3Int((int)(inFrontOfPlayerTrigger.position.x - 0.5f), (int)(inFrontOfPlayerTrigger.position.y - 0.5), (int)transform.position.z));
        if (Input.GetKeyDown(KeyCode.W)) key = KeyCode.W;
        else if (Input.GetKeyDown(KeyCode.A)) key = KeyCode.A;
        else if (Input.GetKeyDown(KeyCode.S)) key = KeyCode.S;
        else if (Input.GetKeyDown(KeyCode.D)) key = KeyCode.D;

        if (Input.GetKeyUp(key)) key = KeyCode.None;

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
            if (!(Vector3.Distance(transform.position, movePoint.position) == 0))
            {
                transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                
                if(IsCollidingNotInFront())
                {
                    selectedTile.transform.position = inFrontOfPlayerTrigger.position;
                    selectedTile.SetActive(true);
                }
                else
                {
                    selectedTile.SetActive(false);
                }
                
                PlayerTurnInPlace();

                dir = GetDirection();
                SetInteractCoordinates(dir);
            }
            

        }
        //Pressed interact
        if (CrossPlatformInputManager.GetButtonDown("Interact") && !tileFlipping)
        {
            GameObject colliding = testTrigger.GetCollidingGameObject();
            if (testTrigger.GetCollidingStatus())
            {
                colliding.GetComponent<IInteractable>().Interact();
            }
        }
        //Pressed tileflip
        if (CrossPlatformInputManager.GetButtonDown("Tileflip") && !interacting)
        {
            //Start flipping
            if(!tileFlipping)
            {
                tileFlipping = true;
                
                StartCoroutine(WaitForFinishWalking());
            }
            //Stop flipping
            else if(tileFlipping && hasFinishedWalking)
            {
                tileFlipping = false;
                Destroy(selectedTile);
                foreach (GameObject tile in availableTiles)
                {
                    Destroy(tile);
                }
                availableTiles.Clear();
            }
        }

        
    }

    private bool IsCollidingNotInFront()
    {
        if ((colliderTilemap.GetTile(new Vector3Int((int)(inFrontOfPlayerTrigger.position.x - 0.5f), (int)(inFrontOfPlayerTrigger.position.y - 0.5), 
            (int)transform.position.z)) == null) && !testTrigger.GetCollidingStatus())
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
        //if(IsCollidingNotInFront())
        //{
        dir = GetDirection();
        SetInteractCoordinates(dir);
        NotWalking();
        selectedTile = Instantiate(selectedTilePrefab, inFrontOfPlayerTrigger.position, Quaternion.identity);
        selectedTile.transform.parent = transform;
        MarkAvailableTiles();
        hasFinishedWalking = true;
        //}
        /*else
        {
            tileFlipping = false;
            hasFinishedWalking = true;
        }*/
        
    }

    private void MarkAvailableTiles()
    {
        Vector3 tempEast =  new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        Vector3 tempWest = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        Vector3 tempNorth = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 tempSouth = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

        //East
        if (colliderTilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f + 1f), (int)(transform.position.y - 0.5),
            (int)transform.position.z)) == null && !Physics2D.OverlapCircle(tempEast, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempEast, Quaternion.identity));
            availableTiles.Add(availableTile);
        }

        //West
        if (colliderTilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f - 1f), (int)(transform.position.y - 0.5),
            (int)transform.position.z)) == null && !Physics2D.OverlapCircle(tempWest, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempWest, Quaternion.identity));
            availableTiles.Add(availableTile);
        }

        //North
        if (colliderTilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5 + 1f),
            (int)transform.position.z)) == null && !Physics2D.OverlapCircle(tempNorth, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempNorth, Quaternion.identity));
            availableTiles.Add(availableTile);
        }

        //South
        if (colliderTilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5 - 1f),
            (int)transform.position.z)) == null && !Physics2D.OverlapCircle(tempSouth, .2f, whatStopsMovement))
        {
            GameObject availableTile = (Instantiate(availableTilePrefab, tempSouth, Quaternion.identity));
            availableTiles.Add(availableTile);
        }
    }

    private void PlayerTurnInPlace()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            currentDirection.x = Input.GetAxisRaw("Horizontal");
            currentDirection.y = Input.GetAxisRaw("Vertical");
        }
        animator.SetFloat("moveX", currentDirection.x);
        animator.SetFloat("moveY", currentDirection.y);
    }

    private void PlayerMove()
    {

        //Actually move the player closer to the movepoint every frame 
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            currentDirection.x = Input.GetAxisRaw("Horizontal");
            currentDirection.y = Input.GetAxisRaw("Vertical");
        }

        //Only check movement input if we are at the movepoint position
        if ((Vector3.Distance(transform.position, movePoint.position) <= .05f))
        {

            //Check if we're pressing all the way to the left or to the right
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && (key == KeyCode.A || key == KeyCode.D || key == KeyCode.None))
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    animator.SetFloat("moveX", currentDirection.x);
                    animator.SetFloat("moveY", 0);

                }
                else
                {
                    NotWalking();
                }
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && (key == KeyCode.W || key == KeyCode.S || key == KeyCode.None))
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, (Input.GetAxisRaw("Vertical")), 0f);
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
    void NotWalking()
    {
        animator.SetBool("Walking", false);
        animator.SetFloat("moveX", currentDirection.x);
        animator.SetFloat("moveY", currentDirection.y);
    }
}
