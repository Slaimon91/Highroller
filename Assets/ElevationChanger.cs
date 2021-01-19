using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevationChanger : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] int newElevation;
    [SerializeField] bool isBridgeEntrance;
    [SerializeField] bool isBridgeExit;
    [SerializeField] bool underneathExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            playerController = other.GetComponent<PlayerController>();
            
            playerController.ElevationChangePlayer(newElevation);

            Debug.Log(transform.parent);

            if(isBridgeEntrance)
            {
                playerController.SetOnBridge(true);
            }
            else if(isBridgeExit)
            {
                playerController.SetOnBridge(false);
            }
            if(underneathExit)
            {
                playerController.SetUnderneath(false);
            }
        }
    }
}
