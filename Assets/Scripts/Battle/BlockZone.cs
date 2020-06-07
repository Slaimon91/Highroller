using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInParent<PlayerBattleController>().EnteredBlockZone(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInParent<PlayerBattleController>().LeavingBlockZone(other.gameObject);
    }
}
