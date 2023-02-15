using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpiderState
{
    Idle,
    Walk,
    Attack,
    Stop
}

public class SpiderController : MonoBehaviour
{
    /* Declaring a constant variable that is used to store the spider's attack distance. */
    const int CHASE_DISTANCE = 20;

    /* Declaring a constant variable that is used to store the spider's attack distance. */
    const int ATTACK_DISTANCE = 7;

    /* A variable that is used to store the player's position. */
    [SerializeField]
    Transform player;

    [SerializeField]
    int spiderWalkSpeed;

    [SerializeField]
    SpiderState spiderState;

    Animator spiderAnimator;

    Vector3 direction;

    [SerializeField]
    Transform shootingPosition;

    [SerializeField]
    GameObject poisonPrefab;

    private void Awake()
    {
        /* The above code is getting the animator component from the spider game object and setting the
        spider state to idle. */
        spiderAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        direction = player.position - transform.position;

        if (
            Vector3.Distance(player.position, transform.position) < CHASE_DISTANCE
            && !GameManager.s_isSpiderDead
        )
        {
            HandleAnimationMovementInArea();
        }
        else
        {
            HandleAnimationMovementOutArea();
        }
    }

    void HandleAnimationMovementInArea()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            0.1f
        );
        spiderAnimator.SetBool("isIdle", false);

        if (direction.magnitude > ATTACK_DISTANCE)
        {
            transform.Translate(0, 0, spiderWalkSpeed * Time.deltaTime);
            spiderAnimator.SetBool("isWalking", true);
            spiderAnimator.SetBool("isAttacking", false);
            spiderState = SpiderState.Walk;
        }
        else
        {
            spiderAnimator.SetBool("isWalking", false);
            spiderAnimator.SetBool("isAttacking", true);
            spiderState = SpiderState.Attack;
        }
    }

    void HandleAnimationMovementOutArea()
    {
        spiderAnimator.SetBool("isIdle", true);
        spiderAnimator.SetBool("isWalking", false);
        spiderAnimator.SetBool("isAttacking", false);
        spiderState = SpiderState.Idle;
    }

    public void DealDamage()
    {
        ShootPoison();
    }

    public void ShootPoison()
    {
        /* The above code is instantiating the poison prefab at the shooting position. */
        GameObject poisonClone = Instantiate(poisonPrefab, shootingPosition.position, shootingPosition.rotation);
        Destroy(poisonClone, 1.0f);

        AudioManager.instance.Play("SpiderHit");
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, CHASE_DISTANCE);
    }
}
