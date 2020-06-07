using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInParent<PlayerBattleController>().EnteredDodgeZone(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInParent<PlayerBattleController>().LeavingDodgeZone(other.gameObject);
    }
}
