using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField] string transitionTo;
    [SerializeField] Vector3 transitionToCoords;
    [SerializeField] Vector2 transitionToRotation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            FindObjectOfType<LevelLoader>().LoadOverworldSceneTransition(transitionTo, transitionToCoords, transitionToRotation);
        }
    }
}
