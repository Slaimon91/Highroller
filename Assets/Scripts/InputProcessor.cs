using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{

    private bool horizontalIsAxisInUse = false;
    private bool verticalIsAxisInUse = false;
    private const float _minimumHeldDuration = 0.25f;
    private float horizontalPressedTime = 0;
    private bool horizontalHeld = false;

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (horizontalIsAxisInUse == false && Input.GetAxisRaw("Horizontal") == -1)
            {
                // Call your event function here.
                horizontalIsAxisInUse = true;
                Debug.Log("Horizontal = -1");
                horizontalPressedTime = Time.timeSinceLevelLoad;
                horizontalHeld = false;
            }
            if (horizontalIsAxisInUse == false && Input.GetAxisRaw("Horizontal") == 1)
            {
                // Call your event function here.
                horizontalIsAxisInUse = true;
                Debug.Log("Horizontal = 1");
                horizontalPressedTime = Time.timeSinceLevelLoad;
                horizontalHeld = false;
            }
        }






        /* if (Input.GetAxisRaw("Vertical") != 0)
         {
             if (verticalIsAxisInUse == false && Input.GetAxisRaw("Vertical") == -1)
             {
                 // Call your event function here.
                 verticalIsAxisInUse = true;
                 Debug.Log("Vertical = -1");
             }
             if (verticalIsAxisInUse == false && Input.GetAxisRaw("Vertical") == 1)
             {
                 // Call your event function here.
                 verticalIsAxisInUse = true;
                 Debug.Log("Vertical = -1");
             }
         }*/

        /*else if (Input.GetAxisRaw("Vertical") == 0)
        {
            verticalIsAxisInUse = false;
            Debug.Log("Vertical Released");
        }*/
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            horizontalIsAxisInUse = false;
           // Debug.Log("Horizontal Released");
            horizontalHeld = false;
        }
        if(horizontalIsAxisInUse == true && Time.timeSinceLevelLoad - horizontalPressedTime > _minimumHeldDuration)
        {
            Debug.Log("Horizontal has been held");
        }
    }
}