using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTitle : MonoBehaviour
{
    [SerializeField] PlayerController.Direction enterDirection;
    [SerializeField] string areaName;
    [SerializeField] AreaTitlebox areaTitlebox;
    private OverworldCanvas overworldCanvas;
    private PlayerController playerController;
    private bool isColliding = false;
    private void Awake()
    {
        overworldCanvas = FindObjectOfType<OverworldCanvas>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerController = other.GetComponent<PlayerController>()) != null)
        {
            if (playerController.GetDirection() == enterDirection)
            {
                AreaTitlebox titleBox = Instantiate(areaTitlebox, overworldCanvas.transform);
                titleBox.AssignInfo(areaName);
            }
        }
    }
}
