using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is to test if we're colliding with game objects, not tilemap
public class InFrontOfPlayerTrigger : MonoBehaviour
{
    [SerializeField] GameObject collidingGameObject = null;
    bool currentlyCollidingInteractable = false;
    bool currentlyCollidingTileable = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            currentlyCollidingInteractable = true;
            collidingGameObject = other.gameObject;
        }

        else if (other.CompareTag("InteractableTile"))
        {
            currentlyCollidingTileable = true;
            collidingGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            currentlyCollidingInteractable = false;
            collidingGameObject = null;
        }

        else if (other.CompareTag("InteractableTile"))
        {
            currentlyCollidingTileable = false;
            collidingGameObject = null;
        }
    }

    public GameObject GetCollidingGameObject()
    {
        return collidingGameObject;
    }

    public bool GetCollidingInteractableStatus()
    {
        return currentlyCollidingInteractable;
    }
    public bool GetCollidingTileableStatus()
    {
        return currentlyCollidingTileable;
    }
}
