using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHelper : MonoBehaviour
{
    public void TriggerCheckpoint()
    {
        GetComponentInParent<CorruptionBar>().UnpauseBox();
    }
}
