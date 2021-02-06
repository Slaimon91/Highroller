using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBase : MonoBehaviour
{
    [SerializeField] protected string trinketName;
    [SerializeField] protected int inventorySlotNr;

    [SerializeField]
    [TextArea(3, 20)]
    protected string info;

    public string GetTrinketName()
    {
        return trinketName;
    }

    public int GetInventorySlotNr()
    {
        return inventorySlotNr;
    }

    public string GetInfo()
    {
        return info;
    }
}
