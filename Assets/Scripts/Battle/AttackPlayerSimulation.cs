using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerSimulation : MonoBehaviour
{
    private bool done = false;
    private bool hitTarget = false;

    public IEnumerator RollAttack(Vector2 target, Vector2 start, float speed)
    {
        while (!hitTarget && !done)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Mathf.Approximately(transform.position.x, target.x))
            {
                hitTarget = true;
                transform.position = FindObjectOfType<BattleSystem>().rightOOB.position;
            }
            yield return null;
        }

        while (hitTarget && !done)
        {

            transform.position = Vector2.MoveTowards(transform.position, start, speed * Time.deltaTime);
            if (Mathf.Approximately(transform.position.x, start.x))
            {
                done = true;
            }
            yield return null;
        }
        yield return null;
        Reset();
    }

    private void Reset()
    {
        done = false;
        hitTarget = false;
    }
}
