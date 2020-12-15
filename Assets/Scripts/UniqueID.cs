using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueID : MonoBehaviour
{
    [HideInInspector]
    public string id { get; private set; }
    private void Awake()
    {
        id = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
    }
}
