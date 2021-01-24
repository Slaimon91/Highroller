using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHelper : MonoBehaviour
{
    public void UnpauseBox()
    {
        GetComponentInParent<CorruptionBar>().UnpauseBox();
    }
    public void UnpauseBoxFinal()
    {
        GetComponentInParent<CorruptionBar>().UnpauseBoxFinal();
    }
}
