using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTab : MonoBehaviour
{
    [SerializeField] GameObject firstSlot;

    public GameObject GetFirstSlot()
    {
        return firstSlot;
    }
}
