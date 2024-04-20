using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public Transform player; 
    public float chaseRange = 6; // Distance within which the enemy starts chasing the player
    private int currentPatrolIndex;
    private Transform targetPatrolPoint;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;
    public float attackRange = 1;
    public float chaseCooldownTime = 5f;
    public float currentCooldownTime = 0;
    public bool isOnCooldown = false;
    Animator _animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        GetNextPatrolPoint();
    }

    private void GetNextPatrolPoint()
    {
        // Ensure the next index is different from the current one
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
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isOnCooldown)//enemy stops 
        {
            currentCooldownTime -= Time.deltaTime;
            if(currentCooldownTime < 4)
            {
                _animator.Play("New anim");
            }
            if (currentCooldownTime <= 0) 
            {
                isOnCooldown = false;
            }
        }
        else
        {
            // If the player is within the chase range, start chasing the player
            if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
            {

                isChasing = true;

                bool isMaze = SceneManager.GetActiveScene().name.StartsWith("Maze");
                navMeshAgent.speed = isMaze ? 1.1f : 2f;
                // code above will set the speed 1.1 in maze, 2 is too much for this scene, rest of the sceenes, it works fine.

                navMeshAgent.angularSpeed = 360;
                _animator.Play("Run_F 0");
                SetDestination(player.position);
            }
            else if(distanceToPlayer <= attackRange)//when enemy reaches the player 
            {
                bool isMaze = SceneManager.GetActiveScene().name.StartsWith("Maze");
                navMeshAgent.speed = isMaze ? 1.1f : 2f;
                // code above will set the speed 1.1 in maze, 2 is too much for this scene, rest of the sceenes, it works fine.

                navMeshAgent.angularSpeed = 360;
                _animator.Play("Victory");
                currentCooldownTime = chaseCooldownTime; //restarting cooldown time
                isOnCooldown = true;
                
            }
            else
            {
                // If not chasing, check if the enemy has reached the target patrol point
                if (!isChasing && navMeshAgent.remainingDistance < 0.1f)
                {
                    // Move to the next patrol point
                    GetNextPatrolPoint();
                }
                else if (isChasing && distanceToPlayer > chaseRange)
                {
                    // If chasing and player goes out of range, stop chasing and go back to patrolling
                    isChasing = false;
                    _animator.Play("Walk_F");
                    navMeshAgent.angularSpeed = 120;
                    GetNextPatrolPoint();
                }
            }
        }
        
    }
}
