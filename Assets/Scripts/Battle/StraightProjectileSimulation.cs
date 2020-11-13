using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectileSimulation : MonoBehaviour
{
    private Vector3 Target;
    [SerializeField] float speed = 5;
    [SerializeField] bool enableAnimation = false;
    [SerializeField] bool destroyAfterHit = true;
    [SerializeField] Transform Projectile;
    private Transform myTransform;
    private Animator animator;

    void Awake()
    {
        myTransform = transform;
    }

    public IEnumerator StartShotCoro()
    {
        yield return StartCoroutine(SimulateProjectileCR(Projectile, myTransform.position, Target));
    }

    public void StartShot()
    {
        StartCoroutine(SimulateProjectileCR(Projectile, myTransform.position, Target));
    }

    IEnumerator SimulateProjectileCR(Transform projectile, Vector3 startPosition, Vector3 endPosition)
    {
        bool hitTarget = false;

        while (!hitTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (Mathf.Approximately(transform.position.x, endPosition.x) && Mathf.Approximately(transform.position.y, endPosition.y))
            {
                hitTarget = true;
                if (enableAnimation)
                    TriggerHitTarget();
                else if(destroyAfterHit)
                    DestroySelf();
            }
            yield return null;
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        Target = newTarget;
    }

    public void TriggerHitTarget()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        animator.SetTrigger("hitTarget");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
