using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    //Public variable to change speed of the player
    public float MovementSpeed = 20f;

    private bool prevStatusVertical;
    private bool prevStatusHorizontal;
    private bool moveHorizontal;

    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        float forwardSpeed = Input.GetAxis("Vertical") * MovementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * MovementSpeed;


        //This variables will be used to check whether player is pressing keys assigned to Horizontal and Vertical
        bool statusHorizontal = Input.GetButton("Horizontal");
        bool statusVertical = Input.GetButton("Vertical");

        if (statusHorizontal && !prevStatusHorizontal)
            moveHorizontal = true;
        if (statusVertical && !prevStatusVertical || !statusHorizontal)
            moveHorizontal = false;

        //By default character is not moving
        Vector3 speed = new Vector3(0, 0, 0);

        if (statusVertical && !moveHorizontal)
        {
            speed.z += forwardSpeed;
        }
        else if (statusHorizontal)
        {
            speed.x += sideSpeed;
        }

        prevStatusVertical = statusVertical;
        prevStatusHorizontal = statusHorizontal;

        cc.SimpleMove(speed);
    }
}
