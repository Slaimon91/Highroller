using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeStart : MonoBehaviour
{
    [SerializeField] PlayerController.Direction enterDirection;
    [SerializeField] int bridgeElevation;
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            if(other.GetComponent<PlayerController>().GetDirection() != enterDirection)
            {
                other.GetComponent<PlayerController>().SetOnBridge(false);
                if(bridgeElevation > 0)
                    other.GetComponent<PlayerController>().ElevationChangePlayer(bridgeElevation, bridgeElevation - 1);
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().SetOnBridge(true);
            if(bridgeElevation > 0)
                other.GetComponent<PlayerController>().ElevationChangePlayer(bridgeElevation - 1, bridgeElevation);
        }
    }
}
