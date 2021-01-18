using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderneathTrigger : MonoBehaviour
{
    private PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            playerController = other.GetComponent<PlayerController>();

            playerController.SetUnderneath(true);
        }
    }
}
