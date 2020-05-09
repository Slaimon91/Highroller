using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class TriggerButtonWithKey : MonoBehaviour
{
    private bool holding = false;

    // Update is called once per frame
    void Update()
    {
        if(CrossPlatformInputManager.GetButtonDown("Submit"))
        {
            holding = true;
            GetComponent<HoldAssignButton>().HoldingButton();
        }
        else if (CrossPlatformInputManager.GetButtonUp("Submit"))
        {
            if(holding)
            {
                holding = false;
                GetComponent<HoldAssignButton>().ReleasedButton();
            }
            
        }
    }
}
