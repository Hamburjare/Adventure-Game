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
}
