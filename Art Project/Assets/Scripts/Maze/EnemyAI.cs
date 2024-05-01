using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public Transform player;
    public float chaseRange = 6; // Distance within which the enemy starts chasing the player
    public float attackRange = 1;
    public float chaseCooldownTime = 5f;
    private float currentCooldownTime = 0;
    private bool isOnCooldown = false;
    private EnemyState currentState = EnemyState.Idle;
    private NavMeshAgent navMeshAgent;
    private Animator _animator;
    private int currentPatrolIndex;
    private Transform targetPatrolPoint;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        GetNextPatrolPoint();
    }

    private void GetNextPatrolPoint()
    {
        int nextIndex = currentPatrolIndex;
        while (nextIndex == currentPatrolIndex)
        {
            nextIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
        }

        currentPatrolIndex = nextIndex;
        targetPatrolPoint = patrolPoints[currentPatrolIndex];
        SetDestination(targetPatrolPoint.position);
    }

    private void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                // Transition to patrol if not already patrolling
                currentState = EnemyState.Patrol; 
                break;

            case EnemyState.Patrol:
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = EnemyState.Chase;
                    break;
                }

                if (navMeshAgent.remainingDistance < 0.1f)
                {
                    GetNextPatrolPoint();
                }
                break;

            case EnemyState.Chase:
                if (distanceToPlayer <= attackRange)
                {
                    currentState = EnemyState.Attack;
                    break;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = EnemyState.Patrol;
                    GetNextPatrolPoint();
                }

                SetDestination(player.position);
                break;

            case EnemyState.Attack:
                if (!isOnCooldown)
                {
                    _animator.Play("Punching");


                    currentCooldownTime = chaseCooldownTime;
                    isOnCooldown = true;
                    // attack logic here

                }
                else
                {
                    currentCooldownTime -= Time.deltaTime;
                    if (currentCooldownTime <= 0)
                    {
                        isOnCooldown = false;
                    }
                }

                if (distanceToPlayer > attackRange)
                {
                    currentState = EnemyState.Chase;
                }
                break;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // Add idle animation
                _animator.Play("Pose_Idle");
                break;
            case EnemyState.Patrol:
                // Add patrol animation
                _animator.Play("Walk_F");

                break;
            case EnemyState.Chase:
                // Add chase animation
                _animator.Play("Run_F 0");

                break;
            case EnemyState.Attack:
                // Add attack animation
                _animator.Play("Punching");

                break;
        }
    }
}