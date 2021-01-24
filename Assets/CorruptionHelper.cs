using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionHelper : MonoBehaviour
{
    public void TriggerCheckpoint()
    {
        GetComponentInParent<CorruptionSource>().DestroyAnimComplete();
    }
}
