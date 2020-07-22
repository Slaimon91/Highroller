using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBase : MonoBehaviour
{
    [SerializeField] protected string trinketName;

    [SerializeField]
    [TextArea(3, 20)]
    protected string info;

    public string GetTrinketName()
    {
        return trinketName;
    }

    public string GetInfo()
    {
        return info;
    }
}
