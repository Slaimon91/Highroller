using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeStart : MonoBehaviour
{
    //[SerializeField] PlayerController.Direction enterDirection;
    [SerializeField] int bridgeElevation;
    private PlayerController playerController;
    [SerializeField] bool isExit;
    /*private void Update()
    {
        if(isColliding)
        {
            Debug.Log("___");
            Collider2D k = Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(0.5f, 0.5f, 0), 0.2f);
            if (k.gameObject.tag != "Player")
            {
                Debug.Log("detecting");
                if (playerController.GetDirection() != enterDirection)
                {
                    playerController.SetOnBridge(false);
                    
                    Debug.Log("Disabled");
                    if (bridgeElevation > 0)
                        playerController.ElevationChangePlayer(bridgeElevation, bridgeElevation - 1);
                }
                isColliding = false;
            }
        }
    }*/
    /*void OnTriggerExit2D(Collider2D other)
    {
        if((playerController = other.GetComponent<PlayerController>()) != null && !isExit)
        {
            if (playerController.GetDirection() != enterDirection)
            {
                playerController.SetOnBridge(false);
                if(bridgeElevation > 0)
                    playerController.ElevationChangePlayer(bridgeElevation, bridgeElevation - 1);
            }
        }
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null && !isExit)
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.SetOnBridge(true);
            if (bridgeElevation > 0)
                playerController.ElevationChangePlayer(bridgeElevation - 1, bridgeElevation);
        }
        else if (other.GetComponent<PlayerController>() != null && isExit)
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.SetOnBridge(false);
            if (bridgeElevation > 0)
                playerController.ElevationChangePlayer(bridgeElevation, bridgeElevation - 1);
        }
    }
}
