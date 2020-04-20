using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform movePoint;
    Vector2 currentDirection;

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
        PlayerMove();
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
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
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

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
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
