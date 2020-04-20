using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform movePoint;
    Vector2 currentDirection;
    bool hasWaited = true;
    bool inputBuffer = false;
    bool playerReady = false;

    private bool horizontalIsAxisInUse = false;
    private bool verticalIsAxisInUse = false;
    private const float _minimumHeldDuration = 0.2f;
    private float horizontalPressedTime = 0;
    private float verticalPressedTime = 0;
    private float movePressedTime = 0;
    private bool horizontalHeld = false;
    private bool verticalHeld = false;

    [SerializeField] LayerMask whatStopsMovement;

    //State

    //Cached component references
    Animator animator;

    void Start()
    {
        movePoint.parent = null;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        TestAxisInput();
        PlayerMove();
    }

    private void TestAxisInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (horizontalIsAxisInUse == false && Input.GetAxisRaw("Horizontal") == 1)
            {
                // Call your event function here.
                horizontalIsAxisInUse = true;
                Debug.Log("Horizontal = 1");
                movePressedTime = Time.timeSinceLevelLoad;
                horizontalHeld = false;
                if (animator.GetFloat("moveX") == 1)
                {
                    //If next tile is not blocked
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {

                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    }
                }

            }
            if (horizontalIsAxisInUse == false && Input.GetAxisRaw("Horizontal") == -1)
            {
                // Call your event function here.
                horizontalIsAxisInUse = true;
                Debug.Log("Horizontal = -1");
                movePressedTime = Time.timeSinceLevelLoad;
                horizontalHeld = false;
                if (animator.GetFloat("moveX") == -1)
                {
                    //If next tile is not blocked
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {

                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    }
                }

            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            horizontalIsAxisInUse = false;
            // Debug.Log("Horizontal Released");
            horizontalHeld = false;
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (verticalIsAxisInUse == false && Input.GetAxisRaw("Vertical") == -1)
            {
                // Call your event function here.
                verticalIsAxisInUse = true;
                Debug.Log("Vertical = -1");
                movePressedTime = Time.timeSinceLevelLoad;
                verticalHeld = false;
                if (animator.GetFloat("moveY") == -1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {

                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    }
                }

            }
            if (verticalIsAxisInUse == false && Input.GetAxisRaw("Vertical") == 1)
            {
                // Call your event function here.
                verticalIsAxisInUse = true;
                Debug.Log("Vertical = 1");
                movePressedTime = Time.timeSinceLevelLoad;
                verticalHeld = false;
                if (animator.GetFloat("moveY") == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    }
                }

            }
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            verticalIsAxisInUse = false;
            // Debug.Log("Horizontal Released");
            verticalHeld = false;
            //movePressedTime = Time.timeSinceLevelLoad;
        }
        if ((horizontalIsAxisInUse == true && Time.timeSinceLevelLoad - movePressedTime > _minimumHeldDuration))
        {
           // Debug.Log("Horizontal has been held");
            //PlayerMove();
            playerReady = true;
            horizontalHeld = true;
        }
        if (verticalIsAxisInUse == true && Time.timeSinceLevelLoad - movePressedTime > _minimumHeldDuration)
        {
           // Debug.Log("Vertical has been held");
            //PlayerMove();
            playerReady = true;
            verticalHeld = true;
        }
        if (horizontalHeld == false && verticalHeld == false)
        {
            playerReady = false;
            Debug.Log("Else triggered");
        }
    }

    private void PlayerMove()
    {

        //Actually move the player closer to the movepoint every frame 
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            currentDirection.x = Input.GetAxisRaw("Horizontal");
            currentDirection.y = Input.GetAxisRaw("Vertical");
            //Debug.Log("X: " + Input.GetAxisRaw("Horizontal") + " Y: " + Input.GetAxisRaw("Vertical"));
        }

        

        //Only check movement input if we are at the movepoint position
        if ((Vector3.Distance(transform.position, movePoint.position) <= .05f))
        {
            animator.SetFloat("moveX", currentDirection.x);
            animator.SetFloat("moveY", currentDirection.y);
            if (playerReady == true)
            {
                //Check if we're pressing all the way to the left or to the right
                if ((Input.GetAxisRaw("Horizontal") == 1f) && animator.GetFloat("moveX") == 1)
                {
                    //If next tile is not blocked
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {

                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    }
                }

                else if ((Input.GetAxisRaw("Horizontal") == -1f) && animator.GetFloat("moveX") == -1)
                {
                    //If next tile is not blocked
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {

                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    }
                }

                else if ((Input.GetAxisRaw("Vertical") == 1f) && animator.GetFloat("moveY") == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {

                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    }
                }

                else if ((Input.GetAxisRaw("Vertical") == -1f) && animator.GetFloat("moveY") == -1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    }
                }
            }
            

            else
            {
                animator.SetBool("Walking", false);
            }

            
        }
        else
        {
            animator.SetBool("Walking", true);
        }
    }
}
