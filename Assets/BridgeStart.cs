using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeStart : MonoBehaviour
{
    [SerializeField] PlayerController.Direction enterDirection;
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            if(other.GetComponent<PlayerController>().GetDirection() != enterDirection)
            {
                other.GetComponent<PlayerController>().ElevationChangePlayer(1, 0);
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().ElevationChangePlayer(0, 1);
        }
    }
}
