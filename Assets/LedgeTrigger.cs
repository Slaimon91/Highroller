using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeTrigger : MonoBehaviour
{
    [SerializeField] PlayerController.Direction direction;
    [SerializeField] Vector3 targetCoordinates;
    PlayerController playerController;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Activate(PlayerController.Direction currDirr)
    {
        if (currDirr == direction)
            playerController.JumpLedge(direction, targetCoordinates);
    }
}
