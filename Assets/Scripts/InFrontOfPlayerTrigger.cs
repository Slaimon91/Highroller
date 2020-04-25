using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfPlayerTrigger : MonoBehaviour
{
    [SerializeField] GameObject collidingGameObject = null;
    bool currentlyColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            currentlyColliding = true;
            collidingGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            currentlyColliding = false;
            collidingGameObject = null;
        }
    }

    public GameObject GetCollidingGameObject()
    {
        return collidingGameObject;
    }
}
