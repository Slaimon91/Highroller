using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is to test if we're colliding with game objects, not tilemap
public class InFrontOfPlayerTrigger : MonoBehaviour
{
    [SerializeField] GameObject collidingGameObject = null;
    bool currentlyCollidingInteractable = false;
    bool currentlyCollidingTileable = false;
    public int collisionCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("InteractableObject"))
        {
            collisionCount++;
            currentlyCollidingInteractable = true;
            collidingGameObject = other.gameObject;
        }

        else if (other.CompareTag("InteractableTile"))
        {
            collisionCount++;
            currentlyCollidingTileable = true;
            collidingGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            collisionCount--;
            if (collisionCount < 0)
                collisionCount = 0;
            if (collisionCount == 0)
            {
                currentlyCollidingInteractable = false;
                collidingGameObject = null;
            }
        }

        else if (other.CompareTag("InteractableTile"))
        {
            collisionCount--;
            if (collisionCount < 0)
                collisionCount = 0;
            if (collisionCount == 0)
            {
                currentlyCollidingTileable = false;
                collidingGameObject = null;
            }
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
    public void ResetCollider()
    {
        collisionCount = 0;
        currentlyCollidingTileable = false;
        collidingGameObject = null;
    }
}
